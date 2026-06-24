using System.Threading.RateLimiting;

namespace FocusFlow.Api.Extensions;

public static class RateLimitingServiceExtensions
{
    public const string AuthPolicy = "auth-policy";

    public static void AddRateLimitingServices(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.ContentType = "application/json";

                var response = new
                {
                    Success = false,
                    StatusCode = StatusCodes.Status429TooManyRequests,
                    Errors = new[]
                    {
                        "Çok fazla istek gönderdiniz. Lütfen biraz sonra tekrar deneyin."
                    }
                };

                await context.HttpContext.Response.WriteAsJsonAsync(
                    response,
                    cancellationToken);
            };

            options.AddPolicy(AuthPolicy, httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: GetClientIpAddress(httpContext),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));
        });
    }

    private static string GetClientIpAddress(HttpContext httpContext)
    {
        return httpContext.Connection.RemoteIpAddress?.ToString()
            ?? "unknown";
    }
}