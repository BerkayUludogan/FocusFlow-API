using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Errors;
using FocusFlow.Api.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.PomodoroSessions.Rules;

public sealed class PomodoroSessionBusinessRules(FocusFlowDbContext dbContext) : IPomodoroSessionBusinessRules
{
    public async Task ClientIdMustBeUniqueAsync(Guid userId, Guid clientId, CancellationToken cancellationToken)
    {
        var clientIdExist = await dbContext.PomodoroSessions
              .AnyAsync(x => x.UserId == userId && x.ClientId == clientId, cancellationToken);

        if (clientIdExist)
            throw new BusinessRuleException(PomodoroSessionErrors.ClientIdAlreadyExists);

    }
    public async Task UserMustNotHaveRunningSessionAsync(Guid userId, CancellationToken cancellationToken)
    {
        var hasRunningSession = await dbContext.PomodoroSessions
            .AnyAsync(x => x.UserId == userId &&
                         x.Status == PomodoroSessionStatus.Running, cancellationToken);

        if (hasRunningSession)
            throw new BusinessRuleException(PomodoroSessionErrors.RunningSessionAlreadyExists);
    }
    public async Task TaskItemMustBelongToUserAsync(Guid userId, Guid? taskItemId, CancellationToken cancellationToken)
    {
        if (taskItemId is null)
            return;

        var taskItemExists = await dbContext.TaskItems
            .AnyAsync(x => x.UserId == userId &&
                         x.Id == taskItemId, cancellationToken);
        if (!taskItemExists)
            throw new BusinessRuleException(PomodoroSessionErrors.TaskItemNotFound);
    }


}
