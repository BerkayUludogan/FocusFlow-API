namespace FocusFlow.Api.Infrastructure.Token;

public sealed class TokenSettings
{
    public string Audience { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string SecurityKey { get; set; } = string.Empty;

    public int TokenExpirationInMinutes { get; set; }

    public int RefreshTokenExpirationInDays { get; set; }
}
