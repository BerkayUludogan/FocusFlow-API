using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Email;
using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Auth.ResendEmailVerificationCode;

public sealed class ResendEmailVerificationCodeCommandHandler(
    FocusFlowDbContext context, 
    IEmailVerificationService emailVerificationService) : IRequestHandler<ResendEmailVerificationCodeCommandRequest, ResendEmailVerificationCodeCommandResponse>
{
    private const int CooldownMinutes = 1;
    private const int LimitWindowMinutes = 15;
    private const int MaxRequestCountInWindow = 3;

    public async Task<ResendEmailVerificationCodeCommandResponse> Handle(ResendEmailVerificationCodeCommandRequest request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await context.Users
             .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

        if (user is null)
        {
            return new ResendEmailVerificationCodeCommandResponse
            {
                Success = true
            };
        }
        if (user.IsEmailVerified)
        {
            return new ResendEmailVerificationCodeCommandResponse
            {
                Success = true
            };
        }

        var now = DateTime.UtcNow;
        var cooldownStart = now.AddMinutes(-CooldownMinutes);
        var limitWindowStart = now.AddMinutes(-LimitWindowMinutes);

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

        if (requestCountInWindow >= MaxRequestCountInWindow)
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
