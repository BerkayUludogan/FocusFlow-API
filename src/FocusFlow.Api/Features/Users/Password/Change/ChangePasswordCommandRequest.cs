using MediatR;

namespace FocusFlow.Api.Features.Users.Password.Change;

public sealed class ChangePasswordCommandRequest : IRequest<ChangePasswordCommandResponse>
{
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
