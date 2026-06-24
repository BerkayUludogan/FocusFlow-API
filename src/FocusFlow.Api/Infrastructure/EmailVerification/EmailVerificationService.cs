using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Infrastructure.Email;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Email;
using FocusFlow.Api.Shared.Abstractions.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace FocusFlow.Api.Infrastructure.EmailVerification;

public sealed class EmailVerificationService(
    FocusFlowDbContext dbContext, 
    IOneTimeCodeService oneTimeCodeService,
    IEmailSender emailSender,
    IOptions<EmailSettings> options)
    : IEmailVerificationService
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendVerificationCodeAsync(
        UserEntity user,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var activeVerificationCodes = await dbContext.UserEmailVerificationCodes
           .Where(code =>
               code.UserId == user.Id &&
               code.UsedAtUtc == null)
           .ToListAsync(cancellationToken);

        foreach (var activeVerificationCode in activeVerificationCodes)
        {
            activeVerificationCode.UsedAtUtc = now;
            activeVerificationCode.ModifiedAtUtc = now;
        }

        var verificationCode = oneTimeCodeService.CreateCode(_settings.VerificationCodeExpirationMinutes);

        var emailVerificationCode = new UserEmailVerificationTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CodeHash = verificationCode.CodeHash,
            ExpiresAtUtc = verificationCode.ExpiresAtUtc
        };

        dbContext.UserEmailVerificationCodes.Add(emailVerificationCode);

        await dbContext.SaveChangesAsync(cancellationToken);

        await emailSender.SendAsync(
            user.Email,
            "FocusFlow email doğrulama kodu",
            BuildEmailBody(user.DisplayName, verificationCode.RawCode, _settings.VerificationCodeExpirationMinutes),
            cancellationToken);
    }

    private static string BuildEmailBody(string displayName, string code, int expirationMinutes)
    {
        return $"""
        <p>Merhaba {WebUtility.HtmlEncode(displayName)},</p>
        <p>FocusFlow hesabını doğrulamak için aşağıdaki kodu uygulamaya gir:</p>
        <h2>{WebUtility.HtmlEncode(code)}</h2>
        <p>Bu kod {expirationMinutes} dakika boyunca geçerlidir.</p>
        """;
    }
}