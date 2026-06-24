using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.PomodoroSessions.GetList;

public sealed class GetPomodoroSessionsQueryHandler
    (FocusFlowDbContext dbContext)
    : IRequestHandler<GetPomodoroSessionsQueryRequest, PagedResponse<GetPomodoroSessionsQueryResponse>>
{
    public async Task<PagedResponse<GetPomodoroSessionsQueryResponse>> Handle(GetPomodoroSessionsQueryRequest request, CancellationToken cancellationToken)
    {
        var query = dbContext.PomodoroSessions
                    .AsNoTracking()
                    .Where(session => session.UserId == request.UserId)
                    .ApplyFilters(request)
                    .OrderByDescending(session => session.StartedAtUtc)
            .Select(session => new GetPomodoroSessionsQueryResponse
            {
                Id = session.Id,
                ClientId = session.ClientId,
                TaskItemId = session.TaskItemId,
                TaskTitle = session.TaskItem != null ? session.TaskItem.Title : null,
                Type = session.Type,
                Status = session.Status,
                StartedAtUtc = session.StartedAtUtc,
                EndedAtUtc = session.EndedAtUtc,
                DurationMinutes = session.DurationMinutes,
                CreatedAtUtc = session.CreatedAtUtc
            });

        return await query.ToPagedResponseAsync(
             request.Page,
             request.PageSize,
             cancellationToken);
    }
}