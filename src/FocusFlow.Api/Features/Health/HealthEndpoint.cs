using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.EndPoints;

namespace FocusFlow.Api.Features.Health;

public sealed class HealthEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/health", () =>
        {
            return Results.Ok(new
            {
                Status = "Healthy",
                TimestampUtc = DateTime.UtcNow
            });

        }).WithTags("Health");

        app.MapGet("/api/health/db", async (
            FocusFlowDbContext context,
            CancellationToken cancellationToken) =>
        {
            var canConnect = await context.Database.CanConnectAsync(cancellationToken);

            if (!canConnect)
            {
                return Results.Json(
                    new
                    {
                        Status = "Unhealthy",
                        Database = "Disconnected",
                        TimestampUtc = DateTime.UtcNow
                    },
                    statusCode: StatusCodes.Status503ServiceUnavailable);
            }

            return Results.Ok(new
            {
                Status = "Healthy",
                Database = "Connected",
                TimestampUtc = DateTime.UtcNow
            });
        })
        .WithTags("Health");

    }
}
