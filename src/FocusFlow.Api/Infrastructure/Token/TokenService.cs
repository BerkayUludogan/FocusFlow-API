using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Features.Auth.DTOs;
using FocusFlow.Api.Shared.Abstractions.Token;
using Microsoft.IdentityModel.Tokens;

namespace FocusFlow.Api.Infrastructure.Token;

public sealed class TokenService(TokenSettings tokenSettings) : ITokenService
{
    public TokenDto CreateToken(UserEntity user)
    {
        var securityKeyValue = tokenSettings.SecurityKey;

        if (string.IsNullOrWhiteSpace(securityKeyValue))
        {
            throw new InvalidOperationException("JWT:SecurityKey missing.");
        }

        var tokenExpirationInMinutes = tokenSettings.TokenExpirationInMinutes;
        var refreshTokenExpirationInDays = tokenSettings.RefreshTokenExpirationInDays;

        var token = new TokenDto
        {
            AccessTokenExpiresAtUtc = DateTime.UtcNow.AddMinutes(tokenExpirationInMinutes),
            RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(refreshTokenExpirationInDays)
        };

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(securityKeyValue));

        var signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.DisplayName)
        };

        var securityToken = new JwtSecurityToken(
            audience: tokenSettings.Audience,
            issuer: tokenSettings.Issuer,
            expires: token.AccessTokenExpiresAtUtc,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: claims);

        var handler = new JwtSecurityTokenHandler();

        token.AccessToken = handler.WriteToken(securityToken);
        token.RefreshToken = CreateRefreshToken();

        return token;
    }

    public string CreateRefreshToken()
    {
        var bytes = new byte[32];

        using var randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }
}