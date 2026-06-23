namespace FocusFlow.Api.Features.Dashboard.Shared;

public static class DashboardPeriodBuilder
{
    public static IReadOnlyList<DashboardPeriodDayResponse> BuildDays(
        DateOnly fromDate,
        int dayCount,
        IReadOnlyList<DashboardSessionSummary> sessions)
    {
        return Enumerable.Range(0, dayCount)
            .Select(offset =>
            {
                var date = fromDate.AddDays(offset);
                var daySessions = sessions
                    .Where(session => session.Date == date)
                    .ToList();

                return new DashboardPeriodDayResponse
                {
                    Date = date,
                    CompletedFocusSessionCount = daySessions.Count,
                    CompletedFocusMinutes = daySessions.Sum(session => session.DurationMinutes)
                };
            })
            .ToList();
    }
}