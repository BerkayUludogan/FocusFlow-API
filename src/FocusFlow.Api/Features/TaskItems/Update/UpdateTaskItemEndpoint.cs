using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Update;

public sealed class UpdateTaskItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/tasks/{id:guid}", async (
            Guid id,
            UpdateTaskItemCommandRequest request,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken) =>
        {

            request.Id = id;
            request.UserId = httpContext.User.GetUserId();

            var response = await sender.Send(request, cancellationToken);

            return Results.Ok(response);

        }).WithTags("Task Items").RequireAuthorization();
    }
}