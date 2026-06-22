namespace FocusFlow.Api.Features.TaskItems.GetById;

public sealed class GetTaskItemByIdQueryResponse
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public DateTime? CompletedAtUtc { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public int EstimatedPomodoroCount { get; set; }

    public int CompletedPomodoroCount { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? ModifiedAtUtc { get; set; }
}
