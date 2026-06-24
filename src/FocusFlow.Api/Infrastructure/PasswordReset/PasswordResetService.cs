using System.Net;
using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Infrastructure.Email;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Email;
using FocusFlow.Api.Shared.Abstractions.PasswordReset;
using FocusFlow.Api.Shared.Abstractions.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FocusFlow.Api.Infrastructure.PasswordReset;

public sealed class PasswordResetService(
    FocusFlowDbContext dbContext,
    IOneTimeCodeService oneTimeCodeService,
    IEmailSender emailSender,
    IOptions<EmailSettings> options)
    : IPasswordResetService
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendPasswordResetCodeAsync(
        UserEntity user,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var activePasswordResetCodes = await dbContext.UserPasswordResetCodes
            .Where(code =>
                code.UserId == user.Id &&
                code.UsedAtUtc == null)
            .ToListAsync(cancellationToken);

        foreach (var activePasswordResetCode in activePasswordResetCodes)
        {
            activePasswordResetCode.UsedAtUtc = now;
            activePasswordResetCode.ModifiedAtUtc = now;
        }

        var passwordResetCode = oneTimeCodeService.CreateCode(
            _settings.PasswordResetCodeExpirationMinutes);

        var passwordResetCodeEntity = new UserPasswordResetCodeEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CodeHash = passwordResetCode.CodeHash,
            ExpiresAtUtc = passwordResetCode.ExpiresAtUtc
        };

        dbContext.UserPasswordResetCodes.Add(passwordResetCodeEntity);

        await dbContext.SaveChangesAsync(cancellationToken);

        await emailSender.SendAsync(
            user.Email,
            "FocusFlow şifre sıfırlama kodu",
            BuildEmailBody(
                user.DisplayName,
                passwordResetCode.RawCode,
                _settings.PasswordResetCodeExpirationMinutes),
            cancellationToken);
    }
    private static string BuildEmailBody(
        string displayName,
        string code,
        int expirationMinutes)
    {
        return $"""
            <p>Merhaba {WebUtility.HtmlEncode(displayName)},</p>
            <p>FocusFlow şifreni sıfırlamak için aşağıdaki kodu uygulamaya gir:</p>
            <h2>{WebUtility.HtmlEncode(code)}</h2>
            <p>Bu kod {expirationMinutes} dakika boyunca geçerlidir.</p>
            """;
    }
}