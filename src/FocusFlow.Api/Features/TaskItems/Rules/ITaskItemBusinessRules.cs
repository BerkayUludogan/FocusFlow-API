namespace FocusFlow.Api.Features.TaskItems.Rules
{
    public interface ITaskItemBusinessRules
    {
        Task ClientIdMustBeUniqueAsync(Guid userId, Guid clientId,
            CancellationToken cancellationToken);
    }
}
