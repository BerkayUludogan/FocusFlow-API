using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Security;
using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Auth.VerifyEmail;

public sealed class VerifyEmailCommandHandler(
    FocusFlowDbContext context,
    IAuthBusinessRules authBusinessRules,
    IOneTimeCodeService oneTimeCodeService)
    : IRequestHandler<VerifyEmailCommandRequest, VerifyEmailCommandResponse>
{
    public async Task<VerifyEmailCommandResponse> Handle(
        VerifyEmailCommandRequest request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var code = request.Code.Trim();

        var user = await context.Users
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

        user = authBusinessRules.UserMustExist(user);

        if (user.IsEmailVerified)
        {
            return new VerifyEmailCommandResponse
            {
                Success = true
            };
        }

        var codeHash = oneTimeCodeService.HashCode(code);

        var verificationCode = await context.UserEmailVerificationCodes
            .Where(verificationCode =>
                verificationCode.UserId == user.Id &&
                verificationCode.CodeHash == codeHash &&
                verificationCode.UsedAtUtc == null)
            .OrderByDescending(verificationCode => verificationCode.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        if (verificationCode is null)
        {
            throw new BusinessRuleException(AuthErrors.EmailVerificationCodeInvalid);
        }

        if (verificationCode.ExpiresAtUtc <= DateTime.UtcNow)
        {
            throw new BusinessRuleException(AuthErrors.EmailVerificationCodeExpired);
        }

        var now = DateTime.UtcNow;

        user.IsEmailVerified = true;
        user.ModifiedAtUtc = now;

        verificationCode.UsedAtUtc = now;
        verificationCode.ModifiedAtUtc = now;

        await context.SaveChangesAsync(cancellationToken);

        return new VerifyEmailCommandResponse
        {
            Success = true
        };
    }
}