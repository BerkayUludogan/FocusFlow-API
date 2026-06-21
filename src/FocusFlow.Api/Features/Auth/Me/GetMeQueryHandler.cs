using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Auth.Me;

public sealed class GetMeQueryHandler(
    FocusFlowDbContext dbContext,
    IAuthBusinessRules rules) : IRequestHandler<GetMeQueryRequest, GetMeQueryResponse>
{
    public async Task<GetMeQueryResponse> Handle(GetMeQueryRequest request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
               .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        user = rules.UserMustExist(user);
        rules.UserMustBeActive(user);

        return new GetMeQueryResponse
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            IsEmailVerified = user.IsEmailVerified,
            CreatedAtUtc = user.CreatedAtUtc
        };

    }
}
