using FocusFlow.Api.Shared.Exceptions;
using System.Text.Json;

namespace FocusFlow.Api.Shared.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment environment)
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
        catch (BaseException ex)
        {
            _logger.LogError(ex, "Uygulama hatası oluştu");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            var response = new
            {
                success = false,
                statusCode = ex.StatusCode,
                errors = ex.Errors
            };
            var result = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Beklenmeyen bir hata oluştu");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errors = new List<string>
                {
                    _environment.IsDevelopment()
                        ? ex.Message
                        : "Beklenmeyen bir hata oluştu"
                };

            var response = new
            {
                success = false,
                statusCode = StatusCodes.Status500InternalServerError,
                errors
            };

            var result = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(result);
        }
    }
}