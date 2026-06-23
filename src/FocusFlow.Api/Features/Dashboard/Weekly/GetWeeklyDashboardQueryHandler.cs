using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Dashboard.Weekly;

public sealed class GetWeeklyDashboardQueryHandler(FocusFlowDbContext dbContext)
    : IRequestHandler<GetWeeklyDashboardQueryRequest, GetWeeklyDashboardQueryResponse>
{
    public async Task<GetWeeklyDashboardQueryResponse> Handle(GetWeeklyDashboardQueryRequest request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var fromDate = today.AddDays(-6);

        var startUtc = fromDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var endUtc = today.AddDays(1).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

        var sessions = await dbContext.PomodoroSessions
           .AsNoTracking()
           .Where(session =>
               session.UserId == request.UserId &&
               session.Type == PomodoroSessionType.Focus &&
               session.Status == PomodoroSessionStatus.Completed &&
               session.StartedAtUtc >= startUtc &&
               session.StartedAtUtc < endUtc)
           .Select(session => new
           {
               Date = DateOnly.FromDateTime(session.StartedAtUtc),
               session.DurationMinutes
           })
           .ToListAsync(cancellationToken);

        var days = Enumerable.Range(0, 7)
            .Select(offset =>
            {
                var date = fromDate.AddDays(offset);
                var daySessions = sessions.Where(session => session.Date == date).ToList();

                return new WeeklyDashboardDayResponse
                {
                    Date = date,
                    CompletedFocusSessionCount = daySessions.Count,
                    CompletedFocusMinutes = daySessions.Sum(session => session.DurationMinutes)
                };
            }).ToList();

        return new GetWeeklyDashboardQueryResponse
        {
            FromDate = fromDate,
            ToDate = today,
            TotalCompletedFocusSessionCount = days.Sum(day => day.CompletedFocusSessionCount),
            TotalFocusMinutes = days.Sum(day => day.CompletedFocusMinutes),
            Days = days
        };
    }
}
