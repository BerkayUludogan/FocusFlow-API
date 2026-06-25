using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.Start
{
    public class StartPomodoroSessionEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/pomodoro-sessions/start", async (
                StartPomodoroSessionRequest request,
                HttpContext httpContext,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new StartPomodoroSessionCommandRequest
                {
                    UserId = httpContext.User.GetUserId(),
                    ClientId = request.ClientId, 
                    TaskItemId = request.TaskItemId,
                    Type = request.Type
                };

                var response = await sender.Send(command, cancellationToken);

                return Results.Created($"/api/pomodoro-sessions/{response.Id}", response);
            }).WithTags("Pomodoro Sessions").RequireAuthorization();
        }
    }
}