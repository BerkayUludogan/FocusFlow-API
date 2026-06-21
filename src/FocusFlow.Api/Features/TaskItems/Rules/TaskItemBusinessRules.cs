using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.TaskItems.Rules;

public sealed class TaskItemBusinessRules(FocusFlowDbContext dbContext) : ITaskItemBusinessRules
{
    public async Task ClientIdMustBeUniqueAsync(Guid userId, Guid clientId, CancellationToken cancellationToken)
    {
        var clientIdExists = await dbContext.TaskItems
            .AnyAsync(
                task => task.UserId == userId &&
                        task.ClientId == clientId,
                cancellationToken);

        if (clientIdExists)
            throw new BusinessRuleException(TaskItemErrors.ClientIdAlreadyExists);
    }
}
