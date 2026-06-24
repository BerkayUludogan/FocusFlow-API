using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/forgot-password", async (
            ForgotPasswordCommandRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);

            return Results.Ok(response);
        })
        .WithTags("Auth");
    }
}