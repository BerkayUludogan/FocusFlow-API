using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.TaskItems.GetList;

public sealed class GetTaskItemsQueryHandler(FocusFlowDbContext dbContext) : IRequestHandler<GetTaskItemsQueryRequest, List<GetTaskItemsQueryResponse>>
{
    public async Task<List<GetTaskItemsQueryResponse>> Handle(GetTaskItemsQueryRequest request, CancellationToken cancellationToken)
    {
        var taskItems = await dbContext.TaskItems
            .AsNoTracking()
            .Where(task => task.UserId == request.UserId)
            .OrderBy(task => task.IsCompleted)
            .ThenByDescending(task => task.CreatedAtUtc)
            .Select(task => new GetTaskItemsQueryResponse
            {
                Id = task.Id,
                ClientId = task.ClientId,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CompletedAtUtc = task.CompletedAtUtc,
                DueDateUtc = task.DueDateUtc,
                EstimatedPomodoroCount = task.EstimatedPomodoroCount,
                CompletedPomodoroCount = task.CompletedPomodoroCount,
                CreatedAtUtc = task.CreatedAtUtc,
                ModifiedAtUtc = task.ModifiedAtUtc
            }).ToListAsync(cancellationToken);

        return taskItems;
    }
}