using FocusFlow.Api.Domain.Enums;

namespace FocusFlow.Api.Features.PomodoroSessions.DTOs;

public sealed class RunningPomodoroSessionDto
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public Guid? TaskItemId { get; set; }

    public string? TaskTitle { get; set; }

    public PomodoroSessionType Type { get; set; }

    public DateTime StartedAtUtc { get; set; }

    public int DurationMinutes { get; set; }
}