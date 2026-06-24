namespace FocusFlow.Api.Infrastructure.Email;

public sealed class EmailSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public bool UseSsl { get; set; }
    public int VerificationCodeExpirationMinutes { get; set; }
    public int VerificationCodeResendCooldownMinutes { get; set; }
    public int VerificationCodeResendLimitWindowMinutes { get; set; }
    public int VerificationCodeMaxRequestCountInWindow { get; set; }

    public int PasswordResetCodeExpirationMinutes { get; set; }
    public int PasswordResetCodeResendCooldownMinutes { get; set; }
    public int PasswordResetCodeResendLimitWindowMinutes { get; set; }
    public int PasswordResetCodeMaxRequestCountInWindow { get; set; }

}