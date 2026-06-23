using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Users.Profile.Update;

public sealed class UpdateUserProfileCommandHandler(
    FocusFlowDbContext dbContext,
    IAuthBusinessRules rules)
    : IRequestHandler<UpdateUserProfileCommandRequest, UpdateUserProfileCommandResponse>
{
    public async Task<UpdateUserProfileCommandResponse> Handle(UpdateUserProfileCommandRequest request, CancellationToken cancellationToken)
    {
        var normalizedDisplayName = request.DisplayName.Trim();

        rules.DisplayNameMustNotBeReserved(normalizedDisplayName);

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        user = rules.UserMustExist(user);

        user.DisplayName = normalizedDisplayName;
        user.ModifiedAtUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateUserProfileCommandResponse
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            ModifiedAtUtc = user.ModifiedAtUtc
        };
    }
}
