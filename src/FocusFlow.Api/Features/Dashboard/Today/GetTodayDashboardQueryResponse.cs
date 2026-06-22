namespace FocusFlow.Api.Features.Dashboard.Today;

public sealed class GetTodayDashboardQueryResponse
{
    public DateOnly Date { get; set; }

    public int CompletedFocusSessionCount { get; set; }

    public int CompletedFocusMinutes { get; set; }

    public int CancelledSessionCount { get; set; }

    public bool HasRunningSession { get; set; }

    public int OpenTaskCount { get; set; }

    public int CompletedTaskCount { get; set; }
}
