using FocusFlow.Api.Domain.Entities;

namespace FocusFlow.Api.Shared.Abstractions.PasswordReset;

public interface IPasswordResetService
{
    Task SendPasswordResetCodeAsync(
        UserEntity user,
        CancellationToken cancellationToken);
}