namespace FocusFlow.Api.Features.Dashboard.Weekly;

public sealed class GetWeeklyDashboardQueryResponse
{
    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public int TotalCompletedFocusSessionCount { get; set; }

    public int TotalFocusMinutes { get; set; }

    public IReadOnlyList<WeeklyDashboardDayResponse> Days { get; set; } = [];
}
public sealed class WeeklyDashboardDayResponse
{
    public DateOnly Date { get; set; }

    public int CompletedFocusSessionCount { get; set; }

    public int CompletedFocusMinutes { get; set; }
}
