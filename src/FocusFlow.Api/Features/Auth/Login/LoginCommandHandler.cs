using FocusFlow.Api.Features.Auth.DTOs;
using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Security;
using FocusFlow.Api.Shared.Abstractions.Token;
using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Auth.Login;

public sealed class LoginCommandHandler(
    FocusFlowDbContext dbContext,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    IAuthBusinessRules loginBusinessRules
    ) : IRequestHandler<LoginCommandRequest, TokenDto>
{
    public async Task<TokenDto> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == normalizedEmail, cancellationToken);

        if (user is null)
            throw new BusinessRuleException(AuthErrors.InvalidCredentials);

        loginBusinessRules.UserMustBeActive(user);

        var isPasswordValid = passwordHasher.Verify(request.Password, user.PasswordHash);

        loginBusinessRules.PasswordMustBeValid(isPasswordValid);
        loginBusinessRules.UserEmailMustBeVerified(user);

        var token = tokenService.CreateToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpiresAtUtc = token.RefreshTokenExpiresAtUtc;
        user.LastLoginAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return token;
    }
}