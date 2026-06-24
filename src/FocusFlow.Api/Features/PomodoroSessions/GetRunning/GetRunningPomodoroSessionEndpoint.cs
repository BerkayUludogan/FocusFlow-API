using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.GetRunning;

public sealed class GetRunningPomodoroSessionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/pomodoro-sessions/running", async (
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var response = await sender.Send(new GetRunningPomodoroSessionQueryRequest
            {
                UserId = httpContext.User.GetUserId()
            }, cancellationToken);
            return Results.Ok(response);

        }).WithTags("Pomodoro Sessions").RequireAuthorization();
    }
}
