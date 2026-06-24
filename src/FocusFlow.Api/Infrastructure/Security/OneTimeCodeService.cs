using System.Security.Cryptography;
using System.Text;
using FocusFlow.Api.Shared.Abstractions.Security;

namespace FocusFlow.Api.Infrastructure.Security;

public sealed class OneTimeCodeService : IOneTimeCodeService
{
    public OneTimeCodeDto CreateCode(int expirationMinutes)
    {
        var rawCode = CreateRawCode();

        return new OneTimeCodeDto
        {
            RawCode = rawCode,
            CodeHash = HashCode(rawCode),
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(expirationMinutes)
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