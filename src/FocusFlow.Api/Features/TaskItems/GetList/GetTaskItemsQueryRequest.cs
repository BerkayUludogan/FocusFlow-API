using MediatR;

namespace FocusFlow.Api.Features.TaskItems.GetList;

public sealed class GetTaskItemsQueryRequest : IRequest<List<GetTaskItemsQueryResponse>>
{
    public Guid UserId { get; set; }
}
