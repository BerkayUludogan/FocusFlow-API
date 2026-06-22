using FocusFlow.Api.Features.TaskItems.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Update;

public sealed class UpdateTaskItemCommandHandler(
    FocusFlowDbContext dbContext,
    ITaskItemBusinessRules taskItemBusinessRules)
    : IRequestHandler<UpdateTaskItemCommandRequest, UpdateTaskItemCommandResponse>
{
    public async Task<UpdateTaskItemCommandResponse> Handle(
        UpdateTaskItemCommandRequest request,
        CancellationToken cancellationToken)
    {
        var taskItem = await taskItemBusinessRules.TaskItemMustExistAsync(
            request.UserId,
            request.Id,
            cancellationToken);

        taskItem.ClientId = request.ClientId;
        taskItem.Title = request.Title;
        taskItem.Description = request.Description;
        taskItem.DueDateUtc = request.DueDateUtc;
        taskItem.EstimatedPomodoroCount = request.EstimatedPomodoroCount;
        taskItem.ModifiedAtUtc = DateTime.UtcNow;

        dbContext.TaskItems.Update(taskItem);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateTaskItemCommandResponse
        {
            Id = taskItem.Id
        };
    }
}