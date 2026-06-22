using FocusFlow.Api.Domain.Entities.Base;
using FocusFlow.Api.Domain.Enums;

namespace FocusFlow.Api.Domain.Entities;

public sealed class PomodoroSessionEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid? TaskItemId { get; set; }

    public Guid ClientId { get; set; }

    public PomodoroSessionType Type { get; set; }

    public PomodoroSessionStatus Status { get; set; }

    public DateTime StartedAtUtc { get; set; }

    public DateTime? EndedAtUtc { get; set; }

    public int DurationMinutes { get; set; }

    public UserEntity User { get; set; } = null!;

    public TaskItemEntity? TaskItem { get; set; }
}
