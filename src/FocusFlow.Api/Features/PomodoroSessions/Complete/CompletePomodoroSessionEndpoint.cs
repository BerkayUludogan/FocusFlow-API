using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.Complete;

public sealed class CompletePomodoroSessionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/pomodoro-sessions/{id:guid}/complete", async (
            Guid id,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var response = await sender.Send(new CompletePomodoroSessionCommandRequest
            {
                Id = id,
                UserId = httpContext.User.GetUserId()
            }, cancellationToken);
            return Results.Ok(response);

        }).WithTags("Pomodoro Sessions").RequireAuthorization();
    }
}