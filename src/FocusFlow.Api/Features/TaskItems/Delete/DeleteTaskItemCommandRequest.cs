using MediatR;

namespace FocusFlow.Api.Features.TaskItems.Delete;

public sealed class DeleteTaskItemCommandRequest : IRequest<DeleteTaskItemCommandResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
