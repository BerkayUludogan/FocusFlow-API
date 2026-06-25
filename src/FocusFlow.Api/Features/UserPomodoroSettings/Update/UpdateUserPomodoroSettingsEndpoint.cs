using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.UserPomodoroSettings.Update;

public sealed class UpdateUserPomodoroSettingsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/me/pomodoro-settings", async (
            UpdateUserPomodoroSettingsRequest request,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateUserPomodoroSettingsCommandRequest
            {
                UserId = httpContext.User.GetUserId(),
                FocusDurationMinutes = request.FocusDurationMinutes,
                ShortBreakDurationMinutes = request.ShortBreakDurationMinutes,
                LongBreakDurationMinutes = request.LongBreakDurationMinutes,
                LongBreakInterval = request.LongBreakInterval,
                DailyFocusGoalMinutes = request.DailyFocusGoalMinutes,
                AutoStartBreaks = request.AutoStartBreaks,
                AutoStartPomodoros = request.AutoStartPomodoros
            };

            var response = await sender.Send(command, cancellationToken);

            return Results.Ok(response);

        }).WithTags("User Pomodoro Settings")
          .RequireAuthorization();
    }
}