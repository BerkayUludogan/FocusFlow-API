using MediatR;

namespace FocusFlow.Api.Features.Auth.Register
{
    public static class RegisterEndpoint
    {
        public static IEndpointRouteBuilder MapRegisterEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/register", async (
                RegisterCommandRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);

                return Results.Created($"/api/users/{response.Id}", response);
            });

            return app;
        }
    }
}
