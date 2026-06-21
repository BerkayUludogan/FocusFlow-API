using MediatR;

namespace FocusFlow.Api.Features.Auth.Me;

public sealed class GetMeQueryRequest : IRequest<GetMeQueryResponse>
{
    public required Guid UserId { get; set; }
}
