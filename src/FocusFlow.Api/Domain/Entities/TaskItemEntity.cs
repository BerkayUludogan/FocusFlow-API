using FocusFlow.Api.Domain.Entities.Base;

namespace FocusFlow.Api.Domain.Entities;

public sealed class TaskItemEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid ClientId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CompletedAtUtc { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public int EstimatedPomodoroCount { get; set; }

    public int CompletedPomodoroCount { get; set; }

    public UserEntity User { get; set; } = null!;
}
