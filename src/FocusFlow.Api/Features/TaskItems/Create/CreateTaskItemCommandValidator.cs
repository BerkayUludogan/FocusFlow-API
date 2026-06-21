using FluentValidation;

namespace FocusFlow.Api.Features.TaskItems.Create;

public sealed class CreateTaskItemCommandValidator : AbstractValidator<CreateTaskItemCommandRequest>
{
    public CreateTaskItemCommandValidator()
    {
        
    }
}
