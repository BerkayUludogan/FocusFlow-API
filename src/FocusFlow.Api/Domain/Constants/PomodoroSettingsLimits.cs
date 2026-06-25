namespace FocusFlow.Api.Domain.Constants;

public static class PomodoroSettingsLimits
{
    public const int MinFocusDurationMinutes = 1;
    public const int MaxFocusDurationMinutes = 180;

    public const int MinShortBreakDurationMinutes = 1;
    public const int MaxShortBreakDurationMinutes = 60;

    public const int MinLongBreakDurationMinutes = 1;
    public const int MaxLongBreakDurationMinutes = 120;

    public const int MinLongBreakInterval = 1;
    public const int MaxLongBreakInterval = 20;

    public const int MinDailyFocusGoalMinutes = 1;
    public const int MaxDailyFocusGoalMinutes = 1440;
}