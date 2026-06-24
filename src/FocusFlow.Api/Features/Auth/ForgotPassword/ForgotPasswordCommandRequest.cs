using MediatR;

namespace FocusFlow.Api.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordCommandRequest : IRequest<ForgotPasswordCommandResponse>
{
    public string Email { get; set; } = string.Empty;
}