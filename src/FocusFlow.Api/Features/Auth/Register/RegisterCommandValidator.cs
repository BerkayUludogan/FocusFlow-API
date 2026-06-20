using FluentValidation;
using FocusFlow.Api.Domain.Constants.FieldLengths;

namespace FocusFlow.Api.Features.Auth.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(UserFieldLengths.Email);

            RuleFor(request => request.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);

            RuleFor(request => request.DisplayName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(UserFieldLengths.DisplayName);
        }
    }
}
