namespace FocusFlow.Api.Features.Dashboard.Shared;

public sealed class DashboardSessionSummary
{
    public DateOnly Date { get; set; }

    public int DurationMinutes { get; set; }
}