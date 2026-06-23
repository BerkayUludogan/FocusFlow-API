using MediatR;

namespace FocusFlow.Api.Features.Dashboard.Weekly;

public sealed class GetWeeklyDashboardQueryRequest : IRequest<GetWeeklyDashboardQueryResponse>
{
    public Guid UserId { get; set; }
}
