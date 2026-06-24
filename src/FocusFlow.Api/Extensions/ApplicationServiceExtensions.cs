using FluentValidation;
using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Features.PomodoroSessions.Rules;
using FocusFlow.Api.Features.TaskItems.Rules;
using FocusFlow.Api.Infrastructure.Security;
using FocusFlow.Api.Shared.Abstractions.Security;
using FocusFlow.Api.Shared.Behaviors;
using MediatR;
using System.Reflection;

namespace FocusFlow.Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddMediatRServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(options =>
            options.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IAuthBusinessRules, AuthBusinessRules>();
        services.AddScoped<ITaskItemBusinessRules, TaskItemBusinessRules>();
        services.AddScoped<IPomodoroSessionBusinessRules, PomodoroSessionBusinessRules>();
        services.AddScoped<IOneTimeCodeService, OneTimeCodeService>();
    }
}