using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Security;
using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Auth.ResetPassword;

public sealed class ResetPasswordCommandHandler(
    FocusFlowDbContext context,
    IAuthBusinessRules authBusinessRules,
    IPasswordHasher passwordHasher,
    IOneTimeCodeService oneTimeCodeService)
    : IRequestHandler<ResetPasswordCommandRequest, ResetPasswordCommandResponse>
{
    public async Task<ResetPasswordCommandResponse> Handle(
        ResetPasswordCommandRequest request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var code = request.Code.Trim();

        var user = await context.Users
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

        user = authBusinessRules.UserMustExist(user);

        authBusinessRules.UserMustBeActive(user);

        var codeHash = oneTimeCodeService.HashCode(code);

        var passwordResetCode = await context.UserPasswordResetCodes
            .Where(resetCode =>
                resetCode.UserId == user.Id &&
                resetCode.CodeHash == codeHash &&
                resetCode.UsedAtUtc == null)
            .OrderByDescending(resetCode => resetCode.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        if (passwordResetCode is null)
        {
            throw new BusinessRuleException(AuthErrors.PasswordResetCodeInvalid);
        }

        if (passwordResetCode.ExpiresAtUtc <= DateTime.UtcNow)
        {
            throw new BusinessRuleException(AuthErrors.PasswordResetCodeExpired);
        }

        var now = DateTime.UtcNow;

        user.PasswordHash = passwordHasher.Hash(request.NewPassword);
        user.RefreshToken = null;
        user.RefreshTokenExpiresAtUtc = null;
        user.ModifiedAtUtc = now;

        passwordResetCode.UsedAtUtc = now;
        passwordResetCode.ModifiedAtUtc = now;

        await context.SaveChangesAsync(cancellationToken);

        return new ResetPasswordCommandResponse
        {
            Success = true
        };
    }
}