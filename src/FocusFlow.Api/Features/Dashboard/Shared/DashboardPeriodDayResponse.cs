namespace FocusFlow.Api.Features.Dashboard.Shared;

public sealed class DashboardPeriodDayResponse
{
    public DateOnly Date { get; set; }

    public int CompletedFocusSessionCount { get; set; }

    public int CompletedFocusMinutes { get; set; }
}