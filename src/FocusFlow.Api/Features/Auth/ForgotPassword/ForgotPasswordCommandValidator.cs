using FluentValidation;
using FocusFlow.Api.Domain.Constants.FieldLengths;

namespace FocusFlow.Api.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordCommandValidator
    : AbstractValidator<ForgotPasswordCommandRequest>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(UserFieldLengths.Email);
    }
}