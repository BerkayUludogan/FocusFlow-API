using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Dashboard.Weekly;

public sealed class GetWeeklyDashboardEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dashboard/weekly", async (
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetWeeklyDashboardQueryRequest
            {
                UserId = httpContext.User.GetUserId()
            }, cancellationToken);

            return Results.Ok(response);

        }).WithTags("Dashboard")
        .RequireAuthorization();
    }
}
