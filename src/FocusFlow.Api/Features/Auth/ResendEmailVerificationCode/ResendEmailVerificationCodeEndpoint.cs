using FocusFlow.Api.Shared.EndPoints;
using MediatR;

namespace FocusFlow.Api.Features.Auth.ResendEmailVerificationCode;

public sealed class ResendEmailVerificationCodeEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/resend-email-verification-code", async (
            ResendEmailVerificationCodeCommandRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);

            return Results.Ok(response);
        })
        .WithTags("Auth");
    }
}