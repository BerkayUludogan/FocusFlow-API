using MediatR;

namespace FocusFlow.Api.Features.TaskItems.GetById;

public sealed class GetTaskItemByIdQueryRequest : IRequest<GetTaskItemByIdQueryResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
