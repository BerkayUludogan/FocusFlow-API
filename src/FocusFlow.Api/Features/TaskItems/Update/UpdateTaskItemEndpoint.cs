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
            UpdateTaskItemRequest request,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateTaskItemCommandRequest
            {
                Id = id,
                UserId = httpContext.User.GetUserId(),
                ClientId = request.ClientId,
                Description = request.Description,
                DueDateUtc = request.DueDateUtc,
                EstimatedPomodoroCount = request.EstimatedPomodoroCount,
                Title = request.Title
            };

            var response = await sender.Send(command, cancellationToken);

            return Results.Ok(response);

        }).WithTags("Task Items").RequireAuthorization();
    }
}