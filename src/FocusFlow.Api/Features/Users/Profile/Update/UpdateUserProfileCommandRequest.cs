using MediatR;

namespace FocusFlow.Api.Features.Users.Profile.Update;

public sealed class UpdateUserProfileCommandRequest : IRequest<UpdateUserProfileCommandResponse>
{
    public Guid UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
}
