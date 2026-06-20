namespace FocusFlow.Api.Features.Auth.Register
{
    public sealed class RegisterCommandResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; }
    }
}
