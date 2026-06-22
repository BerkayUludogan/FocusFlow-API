using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Dashboard.Today;

public sealed class GetTodayDashboardEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dashboard/today", async (
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var response = await sender.Send(new GetTodayDashboardQueryRequest
            {
                UserId = httpContext.User.GetUserId()
            }, cancellationToken);
            return Results.Ok(response);
        }).WithTags("Dashboard").RequireAuthorization();
    }
}
