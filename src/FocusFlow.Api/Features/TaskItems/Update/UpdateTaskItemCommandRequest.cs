using System.Text.Json.Serialization;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Update;

public sealed class UpdateTaskItemCommandRequest : IRequest<UpdateTaskItemCommandResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }

    public Guid ClientId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public int EstimatedPomodoroCount { get; set; }
}