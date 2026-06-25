using FocusFlow.Api.Domain.Constants;
using FocusFlow.Api.Domain.Entities;
using FocusFlow.Api.Domain.Enums;

namespace FocusFlow.Api.Domain.Services;

public static class PomodoroSessionDurationResolver
{
    public static int Resolve(
        PomodoroSessionType type,
        UserPomodoroSettingsEntity? settings)
    {
        return type switch
        {
            PomodoroSessionType.Focus => settings?.FocusDurationMinutes ?? PomodoroSettingsDefaults.FocusDurationMinutes,
            PomodoroSessionType.ShortBreak => settings?.ShortBreakDurationMinutes ?? PomodoroSettingsDefaults.ShortBreakDurationMinutes,
            PomodoroSessionType.LongBreak => settings?.LongBreakDurationMinutes ?? PomodoroSettingsDefaults.LongBreakDurationMinutes,
            _ => PomodoroSettingsDefaults.FocusDurationMinutes
        };
    }
}