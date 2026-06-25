using FluentValidation;
using FocusFlow.Api.Domain.Enums;

namespace FocusFlow.Api.Features.PomodoroSessions.Start;

public sealed class StartPomodoroSessionCommandValidator : AbstractValidator<StartPomodoroSessionCommandRequest>
{
    public StartPomodoroSessionCommandValidator()
    {
        RuleFor(request => request.ClientId)
            .NotEmpty();

        RuleFor(request => request.Type)
            .IsInEnum();
         
        When(request => request.Type == PomodoroSessionType.Focus, () =>
        {
            RuleFor(request => request.TaskItemId)
                .NotEmpty();
        });
    }
}