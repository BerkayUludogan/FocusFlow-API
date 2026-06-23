namespace FocusFlow.Api.Shared.Abstractions.Email;

public class EmailVerificationTokenDto
{
    public string RawToken { get; set; } = string.Empty;

    public string TokenHash { get; set; } = string.Empty;

    public DateTime ExpiresAtUtc { get; set; }
}