using System.Text.Json;
using FocusFlow.Api.Shared.Exceptions;

namespace FocusFlow.Api.Shared.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException exception)
        {
            _logger.LogWarning(exception, "Application exception occurred.");

            await WriteErrorResponseAsync(
                context,
                exception.StatusCode,
                exception.Errors);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unexpected exception occurred.");

            var errors = new List<string>
            {
                _environment.IsDevelopment()
                    ? exception.Message
                    : "Beklenmeyen bir hata oluştu."
            };

            await WriteErrorResponseAsync(
                context,
                StatusCodes.Status500InternalServerError,
                errors);
        }
    }
    private static async Task WriteErrorResponseAsync(
        HttpContext context,
        int statusCode,
        IReadOnlyList<string> errors)
    {
        if (context.Response.HasStarted)
        {
            return;
        }

        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            success = false,
            statusCode,
            errors
        };

        var result = JsonSerializer.Serialize(response, JsonSerializerOptions);

        await context.Response.WriteAsync(result);
    }
}