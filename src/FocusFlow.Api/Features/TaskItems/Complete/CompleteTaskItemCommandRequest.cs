using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Complete;

public sealed class CompleteTaskItemCommandRequest : IRequest<CompleteTaskItemCommandResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
