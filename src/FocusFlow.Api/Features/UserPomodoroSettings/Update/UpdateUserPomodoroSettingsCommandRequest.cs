using MediatR;

namespace FocusFlow.Api.Features.UserPomodoroSettings.Update;

public class UpdateUserPomodoroSettingsCommandRequest : IRequest<UpdateUserPomodoroSettingsCommandResponse>
{
    public Guid UserId { get; set; }

    public int FocusDurationMinutes { get; set; }

    public int ShortBreakDurationMinutes { get; set; }

    public int LongBreakDurationMinutes { get; set; }

    public int LongBreakInterval { get; set; }

    public int DailyFocusGoalMinutes { get; set; }

    public bool AutoStartBreaks { get; set; }

    public bool AutoStartPomodoros { get; set; }
}