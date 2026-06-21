using FluentValidation;

namespace FocusFlow.Api.Features.Auth.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}
