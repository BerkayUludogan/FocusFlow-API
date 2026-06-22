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
                StartPomodoroSessionCommandRequest request,
                HttpContext httpContext,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                request.UserId = httpContext.User.GetUserId();

                var response = await sender.Send(request, cancellationToken);

                return Results.Created($"/api/pomodoro-sessions/{response.Id}", response);
            }).WithTags("Pomodoro Sessions").RequireAuthorization();
        }
    }
}
