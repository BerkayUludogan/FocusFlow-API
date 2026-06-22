using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Delete
{
    public class DeleteTaskItemEndpoint : IEndpoint
    {
        public async void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/api/tasks/{id:guid}", async (
                Guid id,
                HttpContext httpContext,
                ISender sender,
                CancellationToken cancellationToken
                ) =>
            {
                var response = await sender.Send(new DeleteTaskItemCommandRequest
                {
                    Id = id,
                    UserId = httpContext.User.GetUserId()

                }, cancellationToken);
                return Results.Ok(response);

            }).WithTags("Task Items").RequireAuthorization();
        }
    }
}
