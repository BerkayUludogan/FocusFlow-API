using FocusFlow.Api.Infrastructure.Email;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Email;
using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FocusFlow.Api.Features.Auth.ResendEmailVerificationCode;

public sealed class ResendEmailVerificationCodeCommandHandler(
    FocusFlowDbContext context,
    IEmailVerificationService emailVerificationService,
    IOptions<EmailSettings> options) :
    IRequestHandler<ResendEmailVerificationCodeCommandRequest, ResendEmailVerificationCodeCommandResponse>
{
    private readonly EmailSettings _settings = options.Value;
    public async Task<ResendEmailVerificationCodeCommandResponse> Handle(ResendEmailVerificationCodeCommandRequest request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await context.Users
             .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

        if (user is null)
        {
            return new ResendEmailVerificationCodeCommandResponse { Success = true };
        }
        if (user.IsEmailVerified)
        {
            return new ResendEmailVerificationCodeCommandResponse { Success = true };
        }

        var now = DateTime.UtcNow;

        var cooldownStart = now.AddMinutes(-_settings.VerificationCodeResendCooldownMinutes);

        var limitWindowStart = now.AddMinutes(-_settings.VerificationCodeResendLimitWindowMinutes);

        var latestCodeCreatedAtUtc = await context.UserEmailVerificationCodes
            .Where(code => code.UserId == user.Id)
            .OrderByDescending(code => code.CreatedAtUtc)
            .Select(code => (DateTime?)code.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        if (latestCodeCreatedAtUtc is not null && latestCodeCreatedAtUtc > cooldownStart)
        {
            throw new BusinessRuleException(AuthErrors.EmailVerificationCodeRecentlySent);
        }

        var requestCountInWindow = await context.UserEmailVerificationCodes
            .CountAsync(code =>
                code.UserId == user.Id &&
                code.CreatedAtUtc >= limitWindowStart,
                cancellationToken);

        if (requestCountInWindow >= _settings.VerificationCodeMaxRequestCountInWindow)
        {
            throw new BusinessRuleException(AuthErrors.EmailVerificationCodeRequestLimitExceeded);
        }

        await emailVerificationService.SendVerificationCodeAsync(user, cancellationToken);

        return new ResendEmailVerificationCodeCommandResponse
        {
            Success = true
        };
    }
}
