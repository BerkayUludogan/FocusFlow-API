using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.GetRunning;

public sealed class GetRunningPomodoroSessionQueryRequest : IRequest<GetRunningPomodoroSessionQueryResponse>
{ 
    public Guid UserId { get; set; }
}
