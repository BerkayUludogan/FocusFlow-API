using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.GetList;

public sealed class GetTaskItemsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tasks", async (
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var response = await sender.Send(new GetTaskItemsQueryRequest
            {
                UserId = httpContext.User.GetUserId()
            }, cancellationToken);
            return Results.Ok(response);

        }).WithTags("Task Items").RequireAuthorization();
    }
}
