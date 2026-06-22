using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Features.PomodoroSessions.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.Cancel;

public sealed class CancelPomodoroSessionCommandHandler
    (FocusFlowDbContext dbContext, IPomodoroSessionBusinessRules rules)
    : IRequestHandler<CancelPomodoroSessionCommandRequest, CancelPomodoroSessionCommandResponse>
{
    public async Task<CancelPomodoroSessionCommandResponse> Handle(CancelPomodoroSessionCommandRequest request, CancellationToken cancellationToken)
    {
        var pomodoroSession = await rules.PomodoroSessionMustExistAsync(request.UserId, request.Id, cancellationToken);

        rules.PomodoroSessionMustBeRunning(pomodoroSession);

        var now = DateTime.UtcNow;

        pomodoroSession.Status = PomodoroSessionStatus.Cancelled;
        pomodoroSession.EndedAtUtc = now;
        pomodoroSession.ModifiedAtUtc = now;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CancelPomodoroSessionCommandResponse
        {
            Id = pomodoroSession.Id,
            Status = pomodoroSession.Status,
            EndedAtUtc = pomodoroSession.EndedAtUtc
        };
    }
}