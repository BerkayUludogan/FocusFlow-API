using FocusFlow.Api.Infrastructure.Email;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.PasswordReset;
using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FocusFlow.Api.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordCommandHandler(
    FocusFlowDbContext context,
    IPasswordResetService passwordResetService,
    IOptions<EmailSettings> options)
    : IRequestHandler<ForgotPasswordCommandRequest, ForgotPasswordCommandResponse>
{
    private readonly EmailSettings _settings = options.Value;

    public async Task<ForgotPasswordCommandResponse> Handle(
        ForgotPasswordCommandRequest request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await context.Users
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

        if (user is null)
        {
            return new ForgotPasswordCommandResponse
            {
                Success = true
            };
        }

        if (!user.IsActive)
        {
            return new ForgotPasswordCommandResponse
            {
                Success = true
            };
        }

        var now = DateTime.UtcNow;

        var cooldownStart = now.AddMinutes(
            -_settings.PasswordResetCodeResendCooldownMinutes);

        var limitWindowStart = now.AddMinutes(
            -_settings.PasswordResetCodeResendLimitWindowMinutes);

        var latestCodeCreatedAtUtc = await context.UserPasswordResetCodes
            .Where(code => code.UserId == user.Id)
            .OrderByDescending(code => code.CreatedAtUtc)
            .Select(code => (DateTime?)code.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        if (latestCodeCreatedAtUtc is not null && latestCodeCreatedAtUtc > cooldownStart)
        {
            throw new BusinessRuleException(AuthErrors.PasswordResetCodeRecentlySent);
        }

        var requestCountInWindow = await context.UserPasswordResetCodes
            .CountAsync(code =>
                code.UserId == user.Id &&
                code.CreatedAtUtc >= limitWindowStart,
                cancellationToken);

        if (requestCountInWindow >= _settings.PasswordResetCodeMaxRequestCountInWindow)
        {
            throw new BusinessRuleException(AuthErrors.PasswordResetCodeRequestLimitExceeded);
        }

        await passwordResetService.SendPasswordResetCodeAsync(user, cancellationToken);

        return new ForgotPasswordCommandResponse
        {
            Success = true
        };
    }
}