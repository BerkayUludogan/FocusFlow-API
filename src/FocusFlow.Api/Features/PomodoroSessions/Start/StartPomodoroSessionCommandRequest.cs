using FocusFlow.Api.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace FocusFlow.Api.Features.PomodoroSessions.Start;

public sealed class StartPomodoroSessionCommandRequest : IRequest<StartPomodoroSessionCommandResponse>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public Guid ClientId { get; set; }
    public Guid? TaskItemId { get; set; }
    public PomodoroSessionType Type { get; set; }
    public int DurationMinutes { get; set; }
}
