namespace FocusFlow.Api.Features.TaskItems.Complete;

public sealed class CompleteTaskItemCommandResponse
{
    public Guid Id { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAtUtc { get; set; }
}
