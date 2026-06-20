using FluentValidation;
using FocusFlow.Api.Shared.Abstractions.Security;
using FocusFlow.Api.Shared.Behaviors;
using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Infrastructure.Security;
using FocusFlow.Api.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FocusFlow.Api;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);


        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IAuthBusinessRules, AuthBusinessRules>();



        services.AddDbContext<FocusFlowDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}
