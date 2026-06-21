using FocusFlow.Api.Features.Auth.DTOs;
using MediatR;
using System.ComponentModel;

namespace FocusFlow.Api.Features.Auth.Login;

public sealed class LoginCommandRequest : IRequest<TokenDto>
{
    [DefaultValue("buludogan0@gmail.com")]
    public required string Email { get; set; }
    [DefaultValue("Password123!")]
    public required string Password { get; set; }
}

