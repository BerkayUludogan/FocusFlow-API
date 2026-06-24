namespace FocusFlow.Api.Shared.Errors;

public static class AuthErrors
{
    public const string UserNotFound = "Auth.UserNotFound";
    public const string UserNotActive = "Auth.UserNotActive";
    public const string InvalidCredentials = "Auth.InvalidCredentials";
    public const string EmailAlreadyRegistered = "Auth.EmailAlreadyRegistered";
    public const string DisplayNameReserved = "Auth.DisplayNameReserved";
    public const string RefreshTokenNotFound = "Auth.RefreshTokenNotFound";
    public const string RefreshTokenExpired = "Auth.RefreshTokenExpired";
    public const string InvalidToken = "Auth.InvalidToken";
    public const string CurrentPasswordInvalid = "Auth.CurrentPasswordInvalid";
    public const string EmailVerificationCodeInvalid = "Auth.EmailVerificationCodeInvalid";
    public const string EmailVerificationCodeExpired = "Auth.EmailVerificationCodeExpired";
    public const string EmailNotVerified = "Auth.EmailNotVerified";
    public const string EmailVerificationCodeRecentlySent = "Auth.EmailVerificationCodeRecentlySent";
    public const string EmailVerificationCodeRequestLimitExceeded = "Auth.EmailVerificationCodeRequestLimitExceeded";
    public const string PasswordResetCodeRecentlySent = "Auth.PasswordResetCodeRecentlySent";
    public const string PasswordResetCodeRequestLimitExceeded = "Auth.PasswordResetCodeRequestLimitExceeded";
    public const string PasswordResetCodeInvalid = "Auth.PasswordResetCodeInvalid";
    public const string PasswordResetCodeExpired = "Auth.PasswordResetCodeExpired";
}
