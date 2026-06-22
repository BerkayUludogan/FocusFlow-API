using FocusFlow.Api.Domain.Enums;

namespace FocusFlow.Api.Features.PomodoroSessions.Start;

public sealed class StartPomodoroSessionCommandResponse
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public Guid? TaskItemId { get; set; }

    public PomodoroSessionType Type { get; set; }

    public PomodoroSessionStatus Status { get; set; }

    public DateTime StartedAtUtc { get; set; }

    public int DurationMinutes { get; set; }
}
