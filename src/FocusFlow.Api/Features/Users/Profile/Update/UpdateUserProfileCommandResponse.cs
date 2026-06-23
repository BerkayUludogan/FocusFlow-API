namespace FocusFlow.Api.Features.Users.Profile.Update;

public sealed class UpdateUserProfileCommandResponse
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public DateTime? ModifiedAtUtc { get; set; }
}
