using MediatR;

namespace FocusFlow.Api.Features.Dashboard.Monthly;

public sealed class GetMonthlyDashboardQueryRequest : IRequest<GetMonthlyDashboardQueryResponse>
{
    public Guid UserId { get; set; }
}
