using FluentValidation;
using FocusFlow.Api.Domain.Constants.FieldLengths;

namespace FocusFlow.Api.Features.Users.Password.Change;

public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommandRequest>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(request => request.CurrentPassword)
            .NotEmpty()
            .MinimumLength(UserFieldLengths.PasswordMin)
            .MaximumLength(UserFieldLengths.Password);

        RuleFor(request => request.NewPassword)
            .NotEmpty()
            .MinimumLength(UserFieldLengths.PasswordMin)
            .MaximumLength(UserFieldLengths.Password);

        RuleFor(request => request.NewPassword)
            .NotEqual(request => request.CurrentPassword)
            .WithMessage("Yeni şifre mevcut şifre ile aynı olamaz.");
    }
}
