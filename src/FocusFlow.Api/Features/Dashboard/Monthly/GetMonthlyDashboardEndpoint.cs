using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Dashboard.Monthly;

public sealed class GetMonthlyDashboardEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dashboard/monthly", async (
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var response = await sender.Send(new GetMonthlyDashboardQueryRequest
            {
                UserId = httpContext.User.GetUserId()
            }, cancellationToken);
            return Results.Ok(response);
        }).WithTags("Dashboard").RequireAuthorization();
    }
}
