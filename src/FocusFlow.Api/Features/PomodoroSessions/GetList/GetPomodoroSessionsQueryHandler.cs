using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.PomodoroSessions.GetList;

public sealed class GetPomodoroSessionsQueryHandler
    (FocusFlowDbContext dbContext)
    : IRequestHandler<GetPomodoroSessionsQueryRequest, IReadOnlyList<GetPomodoroSessionsQueryResponse>>
{
    public async Task<IReadOnlyList<GetPomodoroSessionsQueryResponse>> Handle(GetPomodoroSessionsQueryRequest request, CancellationToken cancellationToken)
    {
        var query = dbContext.PomodoroSessions
                    .AsNoTracking()
                    .Where(session => session.UserId == request.UserId)
                    .ApplyFilters(request);

        return await query
            .OrderByDescending(x => x.StartedAtUtc)
            .Select(x => new GetPomodoroSessionsQueryResponse
            {
                Id = x.Id,
                ClientId = x.ClientId,
                TaskItemId = x.TaskItemId,
                TaskTitle = x.TaskItem != null ? x.TaskItem.Title : null,
                Type = x.Type,
                Status = x.Status,
                StartedAtUtc = x.StartedAtUtc,
                EndedAtUtc = x.EndedAtUtc,
                DurationMinutes = x.DurationMinutes,
                CreatedAtUtc = x.CreatedAtUtc
            }).ToListAsync(cancellationToken);

    }
}
