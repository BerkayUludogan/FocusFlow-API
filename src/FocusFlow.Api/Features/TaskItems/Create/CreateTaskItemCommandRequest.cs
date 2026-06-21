using MediatR;
using System.Text.Json.Serialization;

namespace FocusFlow.Api.Features.TaskItems.Create;

public sealed class CreateTaskItemCommandRequest : IRequest<CreateTaskItemCommandResponse>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public required Guid ClientId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; } = string.Empty;
    public DateTime? DueDateUtc { get; set; }
    public int EstimatedPomodoroCount { get; set; }
}
