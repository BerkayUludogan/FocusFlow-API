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
}