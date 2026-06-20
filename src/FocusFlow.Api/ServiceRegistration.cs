using FluentValidation;
using FocusFlow.Api.Features.Auth.Register;
using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

        services.AddScoped<IValidator<RegisterCommandRequest>, RegisterCommandValidator>();

        services.AddScoped<IAuthBusinessRules, AuthBusinessRules>();

        services.AddDbContext<FocusFlowDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}
