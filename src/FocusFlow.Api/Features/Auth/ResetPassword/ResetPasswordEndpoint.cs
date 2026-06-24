using FocusFlow.Api.Extensions;
using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Auth.ResetPassword;

public sealed class ResetPasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/reset-password", async (
            ResetPasswordCommandRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);

            return Results.Ok(response);
        })
        .WithTags("Auth").RequireRateLimiting(RateLimitingServiceExtensions.AuthPolicy);
    }
}