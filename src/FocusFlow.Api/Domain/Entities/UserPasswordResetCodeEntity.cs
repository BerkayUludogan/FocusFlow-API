using FocusFlow.Api.Domain.Entities.Base;

namespace FocusFlow.Api.Domain.Entities;

public sealed class UserPasswordResetCodeEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public string CodeHash { get; set; } = string.Empty;

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime? UsedAtUtc { get; set; }

    public UserEntity User { get; set; } = null!;
}
