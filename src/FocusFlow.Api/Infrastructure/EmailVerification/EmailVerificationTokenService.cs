using FocusFlow.Api.Shared.Abstractions.Email;
using System.Security.Cryptography;
using System.Text;

namespace FocusFlow.Api.Infrastructure.EmailVerification;

public sealed class EmailVerificationTokenService : IEmailVerificationTokenService
{
    private const int TokenSizeInBytes = 32;
    private const int ExpirationHours = 24;
    public EmailVerificationTokenDto CreateToken()
    {
        var rawToken = CreateRawToken();

        return new EmailVerificationTokenDto
        {
            RawToken = rawToken,
            TokenHash = HashToken(rawToken),
            ExpiresAtUtc = DateTime.UtcNow.AddHours(ExpirationHours)
        };
    }
    public string HashToken(string token)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var hashBytes = SHA256.HashData(tokenBytes);

        return Convert.ToBase64String(hashBytes);
    }

    private static string CreateRawToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(TokenSizeInBytes);

        return Convert.ToBase64String(bytes);
    }
}