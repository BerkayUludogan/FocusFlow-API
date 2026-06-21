using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Auth.Me;

public class MeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth/me", async (
            HttpContext httpContext,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var userId = httpContext.User.GetUserId();

            var response = await sender.Send(new GetMeQueryRequest { UserId = userId });

            return Results.Ok(response);

        }).WithTags("Auth").RequireAuthorization();
    }
}
