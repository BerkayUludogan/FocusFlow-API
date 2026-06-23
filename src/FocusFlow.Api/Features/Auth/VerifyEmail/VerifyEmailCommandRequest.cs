using MediatR;

namespace FocusFlow.Api.Features.Auth.VerifyEmail;

public sealed class VerifyEmailCommandRequest : IRequest<VerifyEmailCommandResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
