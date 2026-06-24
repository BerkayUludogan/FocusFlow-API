using MediatR;

namespace FocusFlow.Api.Features.Auth.ResetPassword;

public sealed class ResetPasswordCommandRequest : IRequest<ResetPasswordCommandResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}