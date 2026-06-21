using FocusFlow.Api.Domain.Entities;

namespace FocusFlow.Api.Features.Auth.Rules;

public interface IAuthBusinessRules
{
    Task EmailMustBeUniqueAsync(string email, CancellationToken cancellationToken);
    void DisplayNameMustNotBeReserved(string displayName);

    void UserMustBeActive(UserEntity user);
    void PasswordMustBeValid(bool isPasswordValid);
}
