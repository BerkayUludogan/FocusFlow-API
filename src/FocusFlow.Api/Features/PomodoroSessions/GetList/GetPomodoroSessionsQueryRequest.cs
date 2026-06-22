using FocusFlow.Api.Domain.Enums;
using MediatR;

namespace FocusFlow.Api.Features.PomodoroSessions.GetList;

public sealed class GetPomodoroSessionsQueryRequest : IRequest<IReadOnlyList<GetPomodoroSessionsQueryResponse>>
{
    public Guid UserId { get; set; }

    public DateTime? FromUtc { get; set; }

    public DateTime? ToUtc { get; set; }

    public Guid? TaskItemId { get; set; }

    public PomodoroSessionType? Type { get; set; }

    public PomodoroSessionStatus? Status { get; set; }
}
