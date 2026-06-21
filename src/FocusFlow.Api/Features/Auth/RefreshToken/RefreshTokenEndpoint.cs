using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Auth.RefreshToken;

public sealed class RefreshTokenEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/refresh-token", async (
             RefreshTokenCommandRequest request,
            ISender sender,
            CancellationToken cancellationToken
            ) =>
        {
            var response = await sender.Send(request, cancellationToken);
            return Results.Ok(response);
        });
    }
}

