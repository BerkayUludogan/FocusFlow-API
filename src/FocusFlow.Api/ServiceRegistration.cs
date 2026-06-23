using FocusFlow.Api.Extensions; 

namespace FocusFlow.Api;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerServices();
        services.AddJwtAuthentication(configuration);
        services.AddMediatRServices();
        services.AddPersistenceServices(configuration);
        services.AddEmailServices(configuration);
    }
}