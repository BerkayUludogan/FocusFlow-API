using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Features.PomodoroSessions.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Features.PomodoroSessions.Complete;

public sealed class CompletePomodoroSessionCommandHandler(
    FocusFlowDbContext dbContext,
    IPomodoroSessionBusinessRules rules) : IRequestHandler<CompletePomodoroSessionCommandRequest, CompletePomodoroSessionCommandResponse>
{
    public async Task<CompletePomodoroSessionCommandResponse> Handle(CompletePomodoroSessionCommandRequest request, CancellationToken cancellationToken)
    {
        var pomodoroSession = await rules.PomodoroSessionMustExistAsync(request.UserId, request.Id, cancellationToken);
        rules.PomodoroSessionMustBeRunning(pomodoroSession);

        var now = DateTime.UtcNow;

        pomodoroSession.Status = PomodoroSessionStatus.Completed;
        pomodoroSession.EndedAtUtc = now;
        pomodoroSession.ModifiedAtUtc = now;

        int? completedPomodoroCount = null;

        if (pomodoroSession.Type == PomodoroSessionType.Focus &&
            pomodoroSession.TaskItemId is not null)
        {
            var taskItem = await dbContext.TaskItems
                .FirstOrDefaultAsync(
                x => x.Id == pomodoroSession.TaskItemId && x.UserId == request.UserId, cancellationToken);

            if (taskItem is not null)
            {
                taskItem.CompletedPomodoroCount++;
                taskItem.ModifiedAtUtc = now;
                completedPomodoroCount = taskItem.CompletedPomodoroCount;
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CompletePomodoroSessionCommandResponse
        {
            Id = request.Id,
            Status = pomodoroSession.Status,
            EndedAtUtc = pomodoroSession.EndedAtUtc,
            TaskItemId = pomodoroSession.TaskItemId,
            CompletedPomodoroCount = completedPomodoroCount,
        };
    }
}