using FocusFlow.Api.Infrastructure.Email;
using FocusFlow.Api.Infrastructure.EmailVerification;
using FocusFlow.Api.Shared.Abstractions.Email;

namespace FocusFlow.Api.Extensions;

public static class EmailServiceExtensions
{
    public static void AddEmailServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.Configure<EmailSettings>(
            configuration.GetSection("Email"));

        if (environment.IsDevelopment())
        {
            services.AddScoped<IEmailSender, DevelopmentEmailSender>();
        }
        else
        {
            services.AddScoped<IEmailSender, SmtpEmailSender>();
        } 
        services.AddScoped<IEmailVerificationTokenService, EmailVerificationTokenService>();
        services.AddScoped<IEmailVerificationService, EmailVerificationService>();
    }
}