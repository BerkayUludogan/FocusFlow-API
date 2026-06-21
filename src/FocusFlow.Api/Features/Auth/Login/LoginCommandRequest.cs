using FocusFlow.Api.Features.Auth.DTOs;
using MediatR;

namespace FocusFlow.Api.Features.Auth.Login;

public sealed class LoginCommandRequest : IRequest<TokenDto>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

