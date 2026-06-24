using FocusFlow.Api.Features.PomodoroSessions.DTOs;

namespace FocusFlow.Api.Features.PomodoroSessions.GetRunning;

public sealed class GetRunningPomodoroSessionQueryResponse
{
    public bool HasRunningSession { get; set; }
    public RunningPomodoroSessionDto? Session { get; set; }
}
