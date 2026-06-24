using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.GetList;

public sealed class GetTaskItemsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tasks", async (
            int? page,
            int? pageSize,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetTaskItemsQueryRequest
            {
                UserId = httpContext.User.GetUserId(),
                Page = page ?? 1,
                PageSize = pageSize ?? 20
            }, cancellationToken);

            return Results.Ok(response);
        })
        .WithTags("Task Items")
        .RequireAuthorization();
    }
}