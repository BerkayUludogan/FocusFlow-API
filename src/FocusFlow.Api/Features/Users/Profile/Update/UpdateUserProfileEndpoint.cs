using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Users.Profile.Update;

public sealed class UpdateUserProfileEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/profile", async (
            UpdateUserProfileCommandRequest request,
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            request.UserId = httpContext.User.GetUserId();
            var response = await sender.Send(request, cancellationToken);

            return Results.Ok(response);

        }).WithTags("Users").RequireAuthorization();
    }
}
