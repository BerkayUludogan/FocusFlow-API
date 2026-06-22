using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.Cancel;

public sealed class CancelPomodoroSessionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/pomodoro-sessions/{id:guid}/cancel", async (
            Guid id,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new CancelPomodoroSessionCommandRequest
            {
                Id = id,
                UserId = httpContext.User.GetUserId()

            }, cancellationToken);

            return Results.Ok(response);

        }).WithTags("Pomodoro Sessions")
        .RequireAuthorization();
    }
}