namespace FocusFlow.Api.Features.Users.Password.Change;

public sealed class ChangePasswordRequest
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
