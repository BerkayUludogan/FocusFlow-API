using FocusFlow.Api.Domain.Entities;

namespace FocusFlow.Api.Features.PomodoroSessions.Rules;

public interface IPomodoroSessionBusinessRules
{
    Task ClientIdMustBeUniqueAsync(
        Guid userId,
        Guid clientId,
        CancellationToken cancellationToken);

    Task UserMustNotHaveRunningSessionAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task TaskItemMustBelongToUserAsync(
        Guid userId,
        Guid? taskItemId,
        CancellationToken cancellationToken);

    Task<PomodoroSessionEntity> PomodoroSessionMustExistAsync(
    Guid userId,
    Guid pomodoroSessionId,
    CancellationToken cancellationToken);

    void PomodoroSessionMustBeRunning(PomodoroSessionEntity pomodoroSession);
}