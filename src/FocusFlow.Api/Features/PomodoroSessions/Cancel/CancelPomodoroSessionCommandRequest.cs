using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.Cancel;

public sealed class CancelPomodoroSessionCommandRequest : IRequest<CancelPomodoroSessionCommandResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
