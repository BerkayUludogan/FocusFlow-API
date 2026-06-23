using System.Net;
using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Email;

namespace FocusFlow.Api.Infrastructure.EmailVerification;

public sealed class EmailVerificationService(
    FocusFlowDbContext dbContext,
    IEmailVerificationTokenService emailVerificationTokenService,
    IEmailSender emailSender)
    : IEmailVerificationService
{
    public async Task SendVerificationCodeAsync(
        UserEntity user,
        CancellationToken cancellationToken)
    {
        var verificationCode = emailVerificationTokenService.CreateCode();

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
            BuildEmailBody(user.DisplayName, verificationCode.RawCode),
            cancellationToken);
    }

    private static string BuildEmailBody(string displayName, string code)
    {
        return $"""
            <p>Merhaba {WebUtility.HtmlEncode(displayName)},</p>
            <p>FocusFlow hesabını doğrulamak için aşağıdaki kodu uygulamaya gir:</p>
            <h2>{WebUtility.HtmlEncode(code)}</h2>
            <p>Bu kod 10 dakika boyunca geçerlidir.</p>
            """;
    }
}