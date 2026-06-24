using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Users.Profile.Update;

public sealed class UpdateUserProfileEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/profile", async (
            UpdateUserProfileRequest request,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var command = new UpdateUserProfileCommandRequest
            {
                UserId = httpContext.User.GetUserId(),
                DisplayName = request.DisplayName,
            };

            var response = await sender.Send(command, cancellationToken);

            return Results.Ok(response);

        }).WithTags("Users").RequireAuthorization();
    }
}
