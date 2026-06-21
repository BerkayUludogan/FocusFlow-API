using MediatR;

namespace FocusFlow.Api.Features.Auth.Logout;

public sealed class LogoutCommandRequest : IRequest
{
    public Guid UserId { get; set; }
}
