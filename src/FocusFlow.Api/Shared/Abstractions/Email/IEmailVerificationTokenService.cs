namespace FocusFlow.Api.Shared.Abstractions.Email;

public interface IEmailVerificationTokenService
{
    EmailVerificationTokenDto CreateCode();
    string HashCode(string code);
}