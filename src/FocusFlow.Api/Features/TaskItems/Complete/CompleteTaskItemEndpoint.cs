using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Complete;

public sealed class CompleteTaskItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/tasks/{id:guid}/complete", async (
            Guid id,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var response = await sender.Send(new CompleteTaskItemCommandRequest
            {
                Id = id,
                UserId = httpContext.User.GetUserId()
            }, cancellationToken);

            return Results.Ok(response);

        }).WithTags("Task Items").RequireAuthorization();
    }
}
