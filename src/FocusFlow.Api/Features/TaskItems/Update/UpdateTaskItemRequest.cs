namespace FocusFlow.Api.Features.TaskItems.Update;

public sealed class UpdateTaskItemRequest
{
    public Guid ClientId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public int EstimatedPomodoroCount { get; set; }
}
