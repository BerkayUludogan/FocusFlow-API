using FocusFlow.Api.Features.Dashboard.Shared;

namespace FocusFlow.Api.Features.Dashboard.Monthly;

public sealed class GetMonthlyDashboardQueryResponse
{
    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public int TotalCompletedFocusSessionCount { get; set; }

    public int TotalFocusMinutes { get; set; }

    public IReadOnlyList<DashboardPeriodDayResponse> Days { get; set; } = [];
}