using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Auth.Logout;

public sealed class LogoutCommandHandler(
    FocusFlowDbContext dbContext,
    IAuthBusinessRules rules) : IRequestHandler<LogoutCommandRequest>
{
    public async Task Handle(LogoutCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
         .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        user = rules.UserMustExist(user);
        rules.UserMustBeActive(user);

        user.RefreshToken = null;
        user.RefreshTokenExpiresAtUtc = null;

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
