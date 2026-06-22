using MediatR;

namespace FocusFlow.Api.Features.Dashboard.Today;

public sealed class GetTodayDashboardQueryRequest : IRequest<GetTodayDashboardQueryResponse>
{
    public Guid UserId { get; set; }
}
