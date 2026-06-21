using FluentValidation;
using FocusFlow.Api.Domain.Constants.FieldLengths;

namespace FocusFlow.Api.Features.TaskItems.Create;

public sealed class CreateTaskItemCommandValidator : AbstractValidator<CreateTaskItemCommandRequest>
{
    public CreateTaskItemCommandValidator()
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
