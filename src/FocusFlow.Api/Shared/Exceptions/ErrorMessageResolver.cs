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
            [AuthErrors.CurrentPasswordInvalid] = "Mevcut şifre hatalı.",
            [AuthErrors.EmailVerificationCodeInvalid] = "Doğrulama kodu geçersiz.",
            [AuthErrors.EmailVerificationCodeExpired] = "Doğrulama kodunun süresi dolmuş.",
            [AuthErrors.EmailNotVerified] = "Email adresinizi doğrulamanız gerekiyor.",
            [AuthErrors.EmailVerificationCodeRecentlySent] = "Yeni doğrulama kodu istemeden önce biraz beklemelisiniz.",
            [AuthErrors.EmailVerificationCodeRequestLimitExceeded] = "Çok fazla doğrulama kodu istediniz. Lütfen daha sonra tekrar deneyin.",

            [TaskItemErrors.ClientIdAlreadyExists] = "Bu görev zaten senkronize edilmiş.",
            [TaskItemErrors.TaskItemNotFound] = "Görev bulunamadı.",

            [PomodoroSessionErrors.ClientIdAlreadyExists] = "Bu pomodoro oturumu zaten senkronize edilmiş.",
            [PomodoroSessionErrors.RunningSessionAlreadyExists] = "Zaten devam eden bir pomodoro oturumu var.",
            [PomodoroSessionErrors.TaskItemNotFound] = "Göreve bağlı pomodoro oturumu başlatılamadı.",
            [PomodoroSessionErrors.PomodoroSessionNotFound] = "Pomodoro oturumu bulunamadı.",
            [PomodoroSessionErrors.PomodoroSessionAlreadyCompleted] = "Pomodoro oturumu artık aktif değil."

        };

    public static string Get(string key)
    {
        return Messages.TryGetValue(key, out var value)
            ? value
            : key;
    }
}
