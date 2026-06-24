using FocusFlow.Api.Domain.Enums;
using FocusFlow.Api.Shared.Pagination;
using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.GetList;

public sealed class GetPomodoroSessionsQueryRequest : IRequest<PagedResponse<GetPomodoroSessionsQueryResponse>>
{
    public Guid UserId { get; set; }
    public DateTime? FromUtc { get; set; }
    public DateTime? ToUtc { get; set; }
    public Guid? TaskItemId { get; set; }
    public PomodoroSessionType? Type { get; set; }
    public PomodoroSessionStatus? Status { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}