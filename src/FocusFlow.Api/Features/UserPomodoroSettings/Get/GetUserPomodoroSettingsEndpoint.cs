using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.UserPomodoroSettings.Get;

public sealed class GetUserPomodoroSettingsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/me/pomodoro-settings", async (
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetUserPomodoroSettingsQueryRequest
            {
                UserId = httpContext.User.GetUserId()
            }, cancellationToken);

            return Results.Ok(response);

        }).WithTags("User Pomodoro Settings")
          .RequireAuthorization();
    }
}
