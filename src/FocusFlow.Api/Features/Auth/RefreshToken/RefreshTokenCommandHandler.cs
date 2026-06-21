using FocusFlow.Api.Features.Auth.DTOs;
using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Token;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Auth.RefreshToken;

public sealed class RefreshTokenCommandHandler(
    FocusFlowDbContext dbContext,
    ITokenService tokenService, IAuthBusinessRules rules) : IRequestHandler<RefreshTokenCommandRequest, TokenDto>
{
    public async Task<TokenDto> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(
                u => u.RefreshToken == request.RefreshToken,
                cancellationToken
            );

        user = rules.RefreshTokenMustExist(user);
        rules.UserMustBeActive(user);
        rules.RefreshTokenMustNotBeExpired(user);


        var token = tokenService.CreateToken(user);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpiresAtUtc = token.RefreshTokenExpiresAtUtc;

        await dbContext.SaveChangesAsync(cancellationToken);

        return token;
    }
}