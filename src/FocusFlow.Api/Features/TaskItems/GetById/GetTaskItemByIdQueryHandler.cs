using FocusFlow.Api.Features.TaskItems.Rules;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.GetById;

public sealed class GetTaskItemByIdQueryHandler(ITaskItemBusinessRules rules) : IRequestHandler<GetTaskItemByIdQueryRequest, GetTaskItemByIdQueryResponse>
{
    public async Task<GetTaskItemByIdQueryResponse> Handle(GetTaskItemByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var taskItem = await rules.TaskItemMustExistAsync(request.UserId, request.Id, cancellationToken);

        return new GetTaskItemByIdQueryResponse
        {
            Id = taskItem.Id,
            ClientId = taskItem.ClientId,
            Title = taskItem.Title,
            Description = taskItem.Description,
            IsCompleted = taskItem.IsCompleted,
            CompletedAtUtc = taskItem.CompletedAtUtc,
            DueDateUtc = taskItem.DueDateUtc,
            EstimatedPomodoroCount = taskItem.EstimatedPomodoroCount,
            CompletedPomodoroCount = taskItem.CompletedPomodoroCount,
            CreatedAtUtc = taskItem.CreatedAtUtc,
            ModifiedAtUtc = taskItem.ModifiedAtUtc
        };
    }
}
