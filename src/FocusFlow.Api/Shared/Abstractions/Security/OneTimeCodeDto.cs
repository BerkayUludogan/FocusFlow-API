namespace FocusFlow.Api.Shared.Abstractions.Security;

public sealed class OneTimeCodeDto
{
    public string RawCode { get; set; } = string.Empty;

    public string CodeHash { get; set; } = string.Empty;

    public DateTime ExpiresAtUtc { get; set; }
}