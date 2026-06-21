using FocusFlow.Api.Features.Auth.DTOs;
using MediatR;

namespace FocusFlow.Api.Features.Auth.RefreshToken;

public sealed class RefreshTokenCommandRequest : IRequest<TokenDto>
{
    public required string RefreshToken { get; set; }
}
