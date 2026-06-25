using MediatR;

namespace FocusFlow.Api.Features.UserPomodoroSettings.Update;

public sealed class UpdateUserPomodoroSettingsRequest
{
    public int FocusDurationMinutes { get; set; }

    public int ShortBreakDurationMinutes { get; set; }

    public int LongBreakDurationMinutes { get; set; }

    public int LongBreakInterval { get; set; }

    public int DailyFocusGoalMinutes { get; set; }

    public bool AutoStartBreaks { get; set; }

    public bool AutoStartPomodoros { get; set; }
}