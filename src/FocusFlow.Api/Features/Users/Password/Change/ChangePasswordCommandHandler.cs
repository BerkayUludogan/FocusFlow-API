using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Security;
using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Users.Password.Change;

public sealed class ChangePasswordCommandHandler(
    FocusFlowDbContext dbContext,
    IAuthBusinessRules rules,
    IPasswordHasher passwordHasher) : IRequestHandler<ChangePasswordCommandRequest, ChangePasswordCommandResponse>
{
    public async Task<ChangePasswordCommandResponse> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId);

        user = rules.UserMustExist(user);

        var currentPasswordIsValid = passwordHasher.Verify(request.CurrentPassword, user.PasswordHash);

        if (!currentPasswordIsValid)
            throw new BusinessRuleException(AuthErrors.CurrentPasswordInvalid);

        user.PasswordHash = passwordHasher.Hash(request.NewPassword);
        user.RefreshToken = null;
        user.RefreshTokenExpiresAtUtc = null;
        user.ModifiedAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ChangePasswordCommandResponse
        {
            Success = true,
        };
    }
}