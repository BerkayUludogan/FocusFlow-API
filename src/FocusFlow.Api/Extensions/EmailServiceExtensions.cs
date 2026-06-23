using FocusFlow.Api.Infrastructure.Email;
using FocusFlow.Api.Shared.Abstractions.Email;

namespace FocusFlow.Api.Extensions;

public static class EmailServiceExtensions
{
    public static void AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(
            configuration.GetSection("Email"));

        services.AddScoped<IEmailSender, SmtpEmailSender>();
    }
}
