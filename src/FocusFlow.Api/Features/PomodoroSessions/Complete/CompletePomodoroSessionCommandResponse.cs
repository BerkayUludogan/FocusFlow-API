using FocusFlow.Api.Domain.Enums;

namespace FocusFlow.Api.Features.PomodoroSessions.Complete;

public sealed class CompletePomodoroSessionCommandResponse
{
    public Guid Id { get; set; }

    public PomodoroSessionStatus Status { get; set; }

    public DateTime? EndedAtUtc { get; set; }

    public Guid? TaskItemId { get; set; }

    public int? CompletedPomodoroCount { get; set; }
}
