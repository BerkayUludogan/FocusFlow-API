using MediatR;
using System.Text.Json.Serialization;

namespace FocusFlow.Api.Features.Users.Profile.Update;

public sealed class UpdateUserProfileCommandRequest : IRequest<UpdateUserProfileCommandResponse>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
}
