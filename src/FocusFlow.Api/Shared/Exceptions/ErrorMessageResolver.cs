using FocusFlow.Api.Shared.Errors;

namespace FocusFlow.Api.Shared.Exceptions;

public static class ErrorMessageResolver
{
    private static readonly IReadOnlyDictionary<string, string> Messages =
        new Dictionary<string, string>
        {
            [AuthErrors.EmailAlreadyRegistered] = "Bu email zaten kayıtlı.",
            [AuthErrors.DisplayNameReserved] = "Bu kullanıcı adı kullanılamaz.",
            [AuthErrors.UserNotFound] = "Kullanıcı bulunamadı.",
            [AuthErrors.UserNotActive] = "Kullanıcı hesabı aktif değil.",
            [AuthErrors.InvalidCredentials] = "Email veya şifre hatalı.",
            [AuthErrors.RefreshTokenNotFound] = "Refresh token geçersiz.",
            [AuthErrors.RefreshTokenExpired] = "Refresh token geçersiz.",
            [AuthErrors.InvalidToken] = "Geçersiz token.",

            [TaskItemErrors.ClientIdAlreadyExists] = "Bu görev zaten senkronize edilmiş.",
            [TaskItemErrors.TaskItemNotFound] = "Görev bulunamadı."
        };

    public static string Get(string key)
    {
        return Messages.TryGetValue(key, out var value)
            ? value
            : key;
    }
}
