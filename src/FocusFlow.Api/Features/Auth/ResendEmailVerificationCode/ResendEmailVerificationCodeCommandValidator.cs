using FluentValidation;
using FocusFlow.Api.Domain.Constants.FieldLengths;

namespace FocusFlow.Api.Features.Auth.ResendEmailVerificationCode;

public class ResendEmailVerificationCodeCommandValidator : AbstractValidator<ResendEmailVerificationCodeCommandRequest>
{
    public ResendEmailVerificationCodeCommandValidator()
    {
        RuleFor(request => request.Email)
           .NotEmpty()
           .EmailAddress()
           .MaximumLength(UserFieldLengths.Email);
    }
}