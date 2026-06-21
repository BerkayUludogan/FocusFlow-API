using FluentValidation;
using FocusFlow.Api.Domain.Constants.FieldLengths;

namespace FocusFlow.Api.Features.Auth.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
{
    public LoginCommandValidator()
    {
        RuleFor(request => request.Email)
           .NotEmpty()
           .EmailAddress()
           .MaximumLength(UserFieldLengths.Email);

        RuleFor(request => request.Password)
            .NotEmpty()
            .MinimumLength(UserFieldLengths.PasswordMin)
            .MaximumLength(UserFieldLengths.Password);
    }
}

