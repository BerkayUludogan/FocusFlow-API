using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.GetList;

public sealed class GetPomodoroSessionsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/pomodoro-sessions", async (
            DateTime? fromUtc,
            DateTime? toUtc,
            Guid? taskItemId,
            PomodoroSessionType? type,
            PomodoroSessionStatus? status,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var response = await sender.Send(new GetPomodoroSessionsQueryRequest
            {
                UserId = httpContext.User.GetUserId(),
                FromUtc = fromUtc,
                ToUtc = toUtc,
                TaskItemId = taskItemId,
                Type = type,
                Status = status,
            }, cancellationToken);

            return Results.Ok(response);
        }).WithTags("Pomodoro Sessions").RequireAuthorization();
    }
}
