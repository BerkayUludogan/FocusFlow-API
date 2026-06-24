using FocusFlow.Api.Domain.Entities;

namespace FocusFlow.Api.Features.TaskItems.Rules
{
    public interface ITaskItemBusinessRules
    {
        Task ClientIdMustBeUniqueAsync(Guid userId, Guid clientId,
            CancellationToken cancellationToken);
        Task<TaskItemEntity> TaskItemMustExistAsync(Guid userId, Guid taskItemId,
            CancellationToken cancellationToken);
    }
}
