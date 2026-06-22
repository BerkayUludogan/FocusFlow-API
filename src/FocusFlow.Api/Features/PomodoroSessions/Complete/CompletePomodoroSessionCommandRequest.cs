using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.Complete;

public sealed class CompletePomodoroSessionCommandRequest : IRequest<CompletePomodoroSessionCommandResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
