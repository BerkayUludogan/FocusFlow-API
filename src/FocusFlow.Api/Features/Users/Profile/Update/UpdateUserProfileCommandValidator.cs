using FluentValidation;
using FocusFlow.Api.Domain.Constants.FieldLengths;

namespace FocusFlow.Api.Features.Users.Profile.Update;

public sealed class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommandRequest>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(request => request.DisplayName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(UserFieldLengths.DisplayName);
    }
}
