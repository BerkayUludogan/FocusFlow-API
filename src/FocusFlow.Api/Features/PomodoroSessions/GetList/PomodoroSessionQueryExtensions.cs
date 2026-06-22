using FocusFlow.Api.Domain.Entities;

namespace FocusFlow.Api.Features.PomodoroSessions.GetList;

public static class PomodoroSessionQueryExtensions
{
    public static IQueryable<PomodoroSessionEntity> ApplyFilters(
        this IQueryable<PomodoroSessionEntity> query,
        GetPomodoroSessionsQueryRequest request)
    {
        if (request.FromUtc is not null)
            query = query.Where(x => x.StartedAtUtc >= request.FromUtc);
        if (request.ToUtc is not null)
            query = query.Where(x => x.StartedAtUtc <= request.ToUtc);
        if (request.TaskItemId is not null)
            query = query.Where(x => x.TaskItemId == request.TaskItemId);
        if (request.Type is not null)
            query = query.Where(x => x.Type == request.Type);
        if (request.Status is not null)
            query = query.Where(x => x.Status == request.Status);

        return query;
    }
}