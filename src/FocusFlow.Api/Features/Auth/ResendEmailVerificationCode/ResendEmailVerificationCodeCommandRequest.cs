using MediatR;

namespace FocusFlow.Api.Features.Auth.ResendEmailVerificationCode;

public sealed class ResendEmailVerificationCodeCommandRequest : IRequest<ResendEmailVerificationCodeCommandResponse>
{
    public string Email { get; set; } = string.Empty;
}
