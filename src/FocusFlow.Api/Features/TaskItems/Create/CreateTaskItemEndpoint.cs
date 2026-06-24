using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Create;

public sealed class CreateTaskItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tasks", async (
            CreateTaskItemRequest request,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateTaskItemCommandRequest
            {
                UserId = httpContext.User.GetUserId(),
                ClientId = request.ClientId,
                Title = request.Title,
                Description = request.Description,
                DueDateUtc = request.DueDateUtc,
                EstimatedPomodoroCount = request.EstimatedPomodoroCount
            };
            var response = await sender.Send(command, cancellationToken);

            return Results.Created($"/api/tasks/{response.Id}", response);

        }).WithTags("Task Items")
            .RequireAuthorization();
    }
}