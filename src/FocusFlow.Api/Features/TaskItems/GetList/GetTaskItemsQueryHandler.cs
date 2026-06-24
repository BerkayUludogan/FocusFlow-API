using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.TaskItems.GetList;

public sealed class GetTaskItemsQueryHandler(FocusFlowDbContext dbContext)
    : IRequestHandler<GetTaskItemsQueryRequest, PagedResponse<GetTaskItemsQueryResponse>>
{
    public async Task<PagedResponse<GetTaskItemsQueryResponse>> Handle(
        GetTaskItemsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var query = dbContext.TaskItems
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
            });

        return await query.ToPagedResponseAsync(
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}