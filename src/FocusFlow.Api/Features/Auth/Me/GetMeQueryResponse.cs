namespace FocusFlow.Api.Features.Auth.Me;

public sealed class GetMeQueryResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}

