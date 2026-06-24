namespace FocusFlow.Api.Shared.Abstractions.Security;

public interface IOneTimeCodeService
{
    OneTimeCodeDto CreateCode(int expirationMinutes);
    string HashCode(string code);
}