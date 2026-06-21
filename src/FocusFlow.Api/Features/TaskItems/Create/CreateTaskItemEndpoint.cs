using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Create;

public sealed class CreateTaskItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tasks", async (CreateTaskItemCommandRequest request, HttpContext httpContext, ISender sender, CancellationToken cancellationToken) =>
        {
            request.UserId = httpContext.User.GetUserId(); 

            var response = await sender.Send(request, cancellationToken);

            return Results.Created($"/api/tasks/{response.Id}", response);
        }).WithTags("Task Items")
            .RequireAuthorization();
    }
}
