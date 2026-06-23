namespace FocusFlow.Api.Shared.Abstractions.Email;

public interface IEmailVerificationTokenService
{
    EmailVerificationTokenDto CreateToken();

    string HashToken(string token);
}