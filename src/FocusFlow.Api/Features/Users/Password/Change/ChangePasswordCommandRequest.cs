using MediatR;
using System.Text.Json.Serialization;

namespace FocusFlow.Api.Features.Users.Password.Change;

public sealed class ChangePasswordCommandRequest : IRequest<ChangePasswordCommandResponse>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
