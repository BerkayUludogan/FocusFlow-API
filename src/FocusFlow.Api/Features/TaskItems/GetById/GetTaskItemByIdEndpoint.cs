using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.GetById;

public sealed class GetTaskItemByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tasks/{id:guid}", async (
            Guid id,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken)
            =>
        {
            var response = await sender.Send(new GetTaskItemByIdQueryRequest
            {
                Id = id,
                UserId = httpContext.User.GetUserId()
            }, cancellationToken);

            return Results.Ok(response);
        }).WithTags("Task Items").RequireAuthorization();
    }
}
