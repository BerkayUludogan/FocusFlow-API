using FluentValidation;
using FocusFlow.Api.Domain.Constants.FieldLengths;

namespace FocusFlow.Api.Features.Auth.ResetPassword;

public sealed class ResetPasswordCommandValidator
    : AbstractValidator<ResetPasswordCommandRequest>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(UserFieldLengths.Email);

        RuleFor(request => request.Code)
            .NotEmpty()
            .Length(6)
            .Matches("^[0-9]{6}$");

        RuleFor(request => request.NewPassword)
            .NotEmpty()
            .MinimumLength(UserFieldLengths.PasswordMin)
            .MaximumLength(UserFieldLengths.Password);
    }
}