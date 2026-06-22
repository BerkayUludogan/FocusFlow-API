using FluentValidation;
using FocusFlow.Api.Domain.Constants.FieldLengths;

namespace FocusFlow.Api.Features.TaskItems.Update;

public sealed class UpdateTaskItemCommandValidator : AbstractValidator<UpdateTaskItemCommandRequest>
{
    public UpdateTaskItemCommandValidator()
    {
        RuleFor(request => request.ClientId)
            .NotEmpty();

        RuleFor(request => request.Title)
            .NotEmpty()
            .MaximumLength(TaskItemFieldLengths.Title);

        RuleFor(request => request.Description)
            .MaximumLength(TaskItemFieldLengths.Description);

        RuleFor(request => request.EstimatedPomodoroCount)
            .GreaterThanOrEqualTo(0);
    }
}