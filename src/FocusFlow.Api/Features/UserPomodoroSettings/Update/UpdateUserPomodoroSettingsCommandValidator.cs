using FluentValidation;
using FocusFlow.Api.Domain.Constants;

namespace FocusFlow.Api.Features.UserPomodoroSettings.Update;

public sealed class UpdateUserPomodoroSettingsCommandValidator : AbstractValidator<UpdateUserPomodoroSettingsCommandRequest>
{
    public UpdateUserPomodoroSettingsCommandValidator()
    {
        RuleFor(request => request.FocusDurationMinutes)
            .InclusiveBetween(
        PomodoroSettingsLimits.MinFocusDurationMinutes,
        PomodoroSettingsLimits.MaxFocusDurationMinutes);

        RuleFor(request => request.ShortBreakDurationMinutes)
            .InclusiveBetween(
                PomodoroSettingsLimits.MinShortBreakDurationMinutes,
                PomodoroSettingsLimits.MaxShortBreakDurationMinutes);

        RuleFor(request => request.LongBreakDurationMinutes)
            .InclusiveBetween(
                PomodoroSettingsLimits.MinLongBreakDurationMinutes,
                PomodoroSettingsLimits.MaxLongBreakDurationMinutes);

        RuleFor(request => request.LongBreakInterval)
            .InclusiveBetween(
                PomodoroSettingsLimits.MinLongBreakInterval,
                PomodoroSettingsLimits.MaxLongBreakInterval);

        RuleFor(request => request.DailyFocusGoalMinutes)
            .InclusiveBetween(
                PomodoroSettingsLimits.MinDailyFocusGoalMinutes,
                PomodoroSettingsLimits.MaxDailyFocusGoalMinutes);
    }
}