namespace FocusFlow.Api.Shared.Errors;

public static class AuthErrors
{
    public const string UserNotFound = "Auth.UserNotFound";
    public const string UserNotActive = "Auth.UserNotActive";
    public const string InvalidPassword = "Auth.InvalidPassword";
    public const string EmailAlreadyRegistered = "Auth.EmailAlreadyRegistered";
    public const string DisplayNameReserved = "Auth.DisplayNameReserved";
}
