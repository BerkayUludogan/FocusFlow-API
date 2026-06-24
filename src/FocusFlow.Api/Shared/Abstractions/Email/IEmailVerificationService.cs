using FocusFlow.Api.Domain.Entities;

namespace FocusFlow.Api.Shared.Abstractions.Email;

public interface IEmailVerificationService
{
    Task SendVerificationCodeAsync(
        UserEntity user,
        CancellationToken cancellationToken);
}