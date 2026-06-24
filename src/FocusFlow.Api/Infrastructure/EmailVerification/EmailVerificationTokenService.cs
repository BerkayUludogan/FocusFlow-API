using FocusFlow.Api.Infrastructure.Email;
using FocusFlow.Api.Shared.Abstractions.Email;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace FocusFlow.Api.Infrastructure.EmailVerification;

public sealed class EmailVerificationTokenService(IOptions<EmailSettings> options) : IEmailVerificationTokenService
{
    private readonly EmailSettings _settings = options.Value;

    public EmailVerificationTokenDto CreateCode()
    {
        var rawCode = CreateRawCode();

        return new EmailVerificationTokenDto
        {
            RawCode = rawCode,
            CodeHash = HashCode(rawCode),
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_settings.VerificationCodeExpirationMinutes)
        };
    }

    public string HashCode(string code)
    {
        var codeBytes = Encoding.UTF8.GetBytes(code);
        var hashBytes = SHA256.HashData(codeBytes);

        return Convert.ToBase64String(hashBytes);
    }

    private static string CreateRawCode()
    {
        var code = RandomNumberGenerator.GetInt32(100000, 1000000);

        return code.ToString();
    }
}