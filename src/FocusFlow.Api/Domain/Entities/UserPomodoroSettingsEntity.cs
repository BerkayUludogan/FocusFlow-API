using FocusFlow.Api.Domain.Constants;
using FocusFlow.Api.Domain.Entities.Base;

namespace FocusFlow.Api.Domain.Entities;

public sealed class UserPomodoroSettingsEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public int FocusDurationMinutes { get; set; } = PomodoroSettingsDefaults.FocusDurationMinutes;

    public int ShortBreakDurationMinutes { get; set; } = PomodoroSettingsDefaults.ShortBreakDurationMinutes;

    public int LongBreakDurationMinutes { get; set; } = PomodoroSettingsDefaults.LongBreakDurationMinutes;

    public int LongBreakInterval { get; set; } = PomodoroSettingsDefaults.LongBreakInterval;

    public int DailyFocusGoalMinutes { get; set; } = PomodoroSettingsDefaults.DailyFocusGoalMinutes;

    public bool AutoStartBreaks { get; set; } = PomodoroSettingsDefaults.AutoStartBreaks;

    public bool AutoStartPomodoros { get; set; } = PomodoroSettingsDefaults.AutoStartPomodoros;

    public UserEntity User { get; set; } = null!;
}