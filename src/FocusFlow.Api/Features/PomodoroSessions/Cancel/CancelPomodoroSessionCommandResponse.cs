using FocusFlow.Api.Domain.Enums;

namespace FocusFlow.Api.Features.PomodoroSessions.Cancel;

public sealed class CancelPomodoroSessionCommandResponse
{
    public Guid Id { get; set; }
    public PomodoroSessionStatus Status { get; set; }
    public DateTime? EndedAtUtc { get; set; }
}