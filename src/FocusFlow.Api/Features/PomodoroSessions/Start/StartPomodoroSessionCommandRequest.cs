using FocusFlow.Api.Domain.Enums;
using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.Start;

public sealed class StartPomodoroSessionCommandRequest : IRequest<StartPomodoroSessionCommandResponse>
{
    public Guid UserId { get; set; }
    public Guid ClientId { get; set; }
    public Guid? TaskItemId { get; set; }
    public PomodoroSessionType Type { get; set; }
}
