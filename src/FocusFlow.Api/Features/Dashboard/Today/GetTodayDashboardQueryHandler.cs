using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Dashboard.Today;

public sealed class GetTodayDashboardQueryHandler
    (FocusFlowDbContext dbContext)
    : IRequestHandler<GetTodayDashboardQueryRequest, GetTodayDashboardQueryResponse>
{
    public async Task<GetTodayDashboardQueryResponse> Handle(GetTodayDashboardQueryRequest request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var startOfDayUtc = today.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var endOfDayUtc = today.AddDays(1).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

        var completedFocusSessions = dbContext.PomodoroSessions
            .AsNoTracking()
            .Where(x =>
            x.UserId == request.UserId &&
            x.Type == PomodoroSessionType.Focus &&
            x.Status == PomodoroSessionStatus.Completed &&
            x.StartedAtUtc >= startOfDayUtc &&
            x.StartedAtUtc < endOfDayUtc);

        var cancelledSessions = dbContext.PomodoroSessions
            .AsNoTracking()
            .Where(x =>
                x.UserId == request.UserId &&
                x.Status == PomodoroSessionStatus.Cancelled &&
                x.StartedAtUtc >= startOfDayUtc &&
                x.StartedAtUtc < endOfDayUtc);

        var hasRunningSession = await dbContext.PomodoroSessions
            .AsNoTracking()
            .AnyAsync(x => x.UserId == request.UserId &&
                           x.Status == PomodoroSessionStatus.Running, cancellationToken);

        var openTaskCount = await dbContext.TaskItems
            .AsNoTracking()
            .CountAsync(
                task => task.UserId == request.UserId &&
                        !task.IsCompleted,
                cancellationToken);

        var completedTaskCount = await dbContext.TaskItems
            .AsNoTracking()
            .CountAsync(
                task => task.UserId == request.UserId &&
                        task.IsCompleted,
                cancellationToken);

        return new GetTodayDashboardQueryResponse
        {
            Date = today,
            CompletedFocusSessionCount = await completedFocusSessions.CountAsync(cancellationToken),
            CompletedFocusMinutes = await completedFocusSessions.SumAsync(
              session => session.DurationMinutes,
              cancellationToken),
            CancelledSessionCount = await cancelledSessions.CountAsync(cancellationToken),
            HasRunningSession = hasRunningSession,
            OpenTaskCount = openTaskCount,
            CompletedTaskCount = completedTaskCount
        };

    }
}
