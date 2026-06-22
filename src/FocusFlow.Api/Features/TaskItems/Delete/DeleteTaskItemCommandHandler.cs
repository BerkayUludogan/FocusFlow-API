using FocusFlow.Api.Features.TaskItems.Rules;
using FocusFlow.Api.Persistence.Context;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Delete
{
    public class DeleteTaskItemCommandHandler
        (FocusFlowDbContext dbContext, ITaskItemBusinessRules rules) :
        IRequestHandler<DeleteTaskItemCommandRequest, DeleteTaskItemCommandResponse>
    {
        public async Task<DeleteTaskItemCommandResponse> Handle(DeleteTaskItemCommandRequest request, CancellationToken cancellationToken)
        {
            var taskItem = await rules.TaskItemMustExistAsync(request.UserId, request.Id, cancellationToken);

            taskItem.IsDeleted = true;
            taskItem.ModifiedAtUtc = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteTaskItemCommandResponse
            {
                Id = request.Id,
            };
        }
    }
}