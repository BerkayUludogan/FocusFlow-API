using FocusFlow.Api.Domain.Entities.Base;

namespace FocusFlow.Api.Domain.Entities
{
    public sealed class UserEntity : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAtUtc { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginAtUtc { get; set; }
    }
}
