namespace FocusFlow.Api.Shared.Abstractions.Email;

public sealed class EmailVerificationTokenDto
{
    public string RawCode { get; set; } = string.Empty;

    public string CodeHash { get; set; } = string.Empty;

    public DateTime ExpiresAtUtc { get; set; }
}