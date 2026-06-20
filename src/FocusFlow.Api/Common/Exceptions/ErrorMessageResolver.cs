using FocusFlow.Api.Common.Errors;

namespace FocusFlow.Api.Common.Exceptions;

public class ErrorMessageResolver
{
    private static readonly IReadOnlyDictionary<string, string> Messages =
       new Dictionary<string, string>
       {
           [AuthErrors.EmailAlreadyRegistered] = "Bu email zaten kayıtlı.",
           [AuthErrors.DisplayNameReserved] = "Bu kullanıcı adı kullanılamaz.",
       };
    public static string Get(string key)
           => Messages.TryGetValue(key, out var value)
               ? value
               : key;
}
