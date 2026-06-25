using FocusFlow.Api.Domain.Enums;

namespace FocusFlow.Api.Features.PomodoroSessions.Start;

public sealed class StartPomodoroSessionRequest
{
    public Guid ClientId { get; set; }
    public Guid? TaskItemId { get; set; }
    public PomodoroSessionType Type { get; set; } 
}
