using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Auth.Logout;

public sealed class LogoutEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/logout", async (
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var userId = httpContext.User.GetUserId();

            await sender.Send(new LogoutCommandRequest
            {
                UserId = userId,
            }, cancellationToken);

            return Results.NoContent();

        }).RequireAuthorization();
    }
}