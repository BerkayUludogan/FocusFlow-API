using Microsoft.OpenApi;

namespace FocusFlow.Api.Extensions;

public static class SwaggerServiceExtensions
{
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FocusFlow Web Api",
                Version = "v1",
                License = new OpenApiLicense
                {
                    Name = "Powered by Berkay Uludoğan"
                },
                Contact = new OpenApiContact
                {
                    Name = "Berkay Uludoğan",
                    Email = "buludogan0@gmail.com"
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization: Bearer {token}"
            });

            options.AddSecurityRequirement(document =>
                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
                });
        });
    }
}