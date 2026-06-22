using FocusFlow.Api.Features.TaskItems.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Complete;

public sealed class CompleteTaskItemCommandHandler(
    FocusFlowDbContext dbContext,
    ITaskItemBusinessRules rules) : IRequestHandler<CompleteTaskItemCommandRequest, CompleteTaskItemCommandResponse>
{
    public async Task<CompleteTaskItemCommandResponse> Handle(CompleteTaskItemCommandRequest request, CancellationToken cancellationToken)
    {
        var taskItem = await rules.TaskItemMustExistAsync(request.UserId, request.Id, cancellationToken);

        var now = DateTime.UtcNow;

        taskItem.IsCompleted = true;
        taskItem.CompletedAtUtc = now;
        taskItem.ModifiedAtUtc = now;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CompleteTaskItemCommandResponse
        {
            Id = taskItem.Id,
            IsCompleted = taskItem.IsCompleted,
            CompletedAtUtc = taskItem.CompletedAtUtc,
        };
    }
}