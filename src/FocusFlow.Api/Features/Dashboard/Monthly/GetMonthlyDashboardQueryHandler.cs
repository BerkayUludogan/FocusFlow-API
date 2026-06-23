using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Features.Dashboard.Shared;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.Dashboard.Monthly;

public sealed class GetMonthlyDashboardQueryHandler(FocusFlowDbContext dbContext)
    : IRequestHandler<GetMonthlyDashboardQueryRequest, GetMonthlyDashboardQueryResponse>
{
    public async Task<GetMonthlyDashboardQueryResponse> Handle(GetMonthlyDashboardQueryRequest request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var fromDate = today.AddDays(-29);

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
            .Select(session => new DashboardSessionSummary
            {
                Date = DateOnly.FromDateTime(session.StartedAtUtc),
                DurationMinutes = session.DurationMinutes
            })
            .ToListAsync(cancellationToken);

        var days = DashboardPeriodBuilder.BuildDays(
            fromDate,
            30,
            sessions);

        return new GetMonthlyDashboardQueryResponse
        {
            FromDate = fromDate,
            ToDate = today,
            TotalCompletedFocusSessionCount = days.Sum(day => day.CompletedFocusSessionCount),
            TotalFocusMinutes = days.Sum(day => day.CompletedFocusMinutes),
            Days = days
        };

    }
}
