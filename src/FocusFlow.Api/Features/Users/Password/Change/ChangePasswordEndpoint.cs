using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Users.Password.Change;

public sealed class ChangePasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/password", async (
            ChangePasswordCommandRequest request,
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
