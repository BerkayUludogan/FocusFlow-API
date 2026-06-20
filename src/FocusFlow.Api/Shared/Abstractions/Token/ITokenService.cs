using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Features.Auth.DTOs;

namespace FocusFlow.Api.Shared.Abstractions.Token;

public interface ITokenService
{
    TokenDto CreateToken(UserEntity User);
    string CreateRefreshToken();
}
