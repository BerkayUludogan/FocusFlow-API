using FocusFlow.Api.Domain.Entities; 
using FocusFlow.Api.Features.TaskItems.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR; 

namespace FocusFlow.Api.Features.TaskItems.Create;

public sealed class CreateTaskItemCommandHandler(
    FocusFlowDbContext dbContext,
    ITaskItemBusinessRules rules
    ) : IRequestHandler<CreateTaskItemCommandRequest, CreateTaskItemCommandResponse>
{
    public async Task<CreateTaskItemCommandResponse> Handle(CreateTaskItemCommandRequest request, CancellationToken cancellationToken)
    {
        await rules.ClientIdMustBeUniqueAsync(
            request.UserId, request.ClientId, cancellationToken);

        var taskItem = new TaskItemEntity
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            ClientId = request.ClientId,
            Title = request.Title.Trim(),
            Description = request.Description?.Trim(),
            DueDateUtc = request.DueDateUtc,
            EstimatedPomodoroCount = request.EstimatedPomodoroCount,
            CompletedPomodoroCount = 0,
            IsCompleted = false
        };

        dbContext.TaskItems.Add(taskItem);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTaskItemCommandResponse
        {
            Id = taskItem.Id,
            ClientId = taskItem.ClientId,
            Title = taskItem.Title,
            Description = taskItem.Description,
            IsCompleted = taskItem.IsCompleted,
            DueDateUtc = taskItem.DueDateUtc,
            EstimatedPomodoroCount = taskItem.EstimatedPomodoroCount,
            CompletedPomodoroCount = taskItem.CompletedPomodoroCount,
            CreatedAtUtc = taskItem.CreatedAtUtc
        };
    }
}