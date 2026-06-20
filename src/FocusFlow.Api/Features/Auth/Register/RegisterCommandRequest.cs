using MediatR;

namespace FocusFlow.Api.Features.Auth.Register
{
    public sealed class RegisterCommandRequest : IRequest<RegisterCommandResponse>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string DisplayName { get; set; }
    }
}
