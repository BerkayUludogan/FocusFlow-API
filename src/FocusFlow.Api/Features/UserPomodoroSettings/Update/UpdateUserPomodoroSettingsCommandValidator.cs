using FluentValidation;

namespace FocusFlow.Api.Features.UserPomodoroSettings.Update;

public sealed class UpdateUserPomodoroSettingsCommandValidator : AbstractValidator<UpdateUserPomodoroSettingsCommandRequest>
{
    public UpdateUserPomodoroSettingsCommandValidator()
    {
        RuleFor(request => request.FocusDurationMinutes)
            .InclusiveBetween(1, 180);

        RuleFor(request => request.ShortBreakDurationMinutes)
            .InclusiveBetween(1, 60);

        RuleFor(request => request.LongBreakDurationMinutes)
            .InclusiveBetween(1, 120);

        RuleFor(request => request.LongBreakInterval)
            .InclusiveBetween(1, 20);

        RuleFor(request => request.DailyFocusGoalMinutes)
            .InclusiveBetween(1, 1440);
    }
}