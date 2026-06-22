using FocusFlow.Api.Domain.Enums;

namespace FocusFlow.Api.Features.PomodoroSessions.GetList;

public sealed class GetPomodoroSessionsQueryResponse
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public Guid? TaskItemId { get; set; }

    public string? TaskTitle { get; set; }

    public PomodoroSessionType Type { get; set; }

    public PomodoroSessionStatus Status { get; set; }

    public DateTime StartedAtUtc { get; set; }

    public DateTime? EndedAtUtc { get; set; }

    public int DurationMinutes { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}
