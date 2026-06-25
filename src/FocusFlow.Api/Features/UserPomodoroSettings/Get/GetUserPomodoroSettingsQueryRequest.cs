using MediatR;

namespace FocusFlow.Api.Features.UserPomodoroSettings.Get;

public sealed class GetUserPomodoroSettingsQueryRequest : IRequest<GetUserPomodoroSettingsQueryResponse>
{
    public Guid UserId { get; set; }
}
