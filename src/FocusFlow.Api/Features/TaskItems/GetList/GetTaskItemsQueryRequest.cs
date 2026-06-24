using FocusFlow.Api.Shared.Pagination;
using MediatR;

namespace FocusFlow.Api.Features.TaskItems.GetList;

public sealed class GetTaskItemsQueryRequest : IRequest<PagedResponse<GetTaskItemsQueryResponse>>
{
    public Guid UserId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
