using FocusFlow.Api.Infrastructure.Email;
using FocusFlow.Api.Infrastructure.EmailVerification;
using FocusFlow.Api.Infrastructure.PasswordReset;
using FocusFlow.Api.Shared.Abstractions.Email;
using FocusFlow.Api.Shared.Abstractions.PasswordReset;

namespace FocusFlow.Api.Extensions;

public static class EmailServiceExtensions
{
    public static void AddEmailServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddOptions<EmailSettings>()
            .Bind(configuration.GetSection("Email"))
            .Validate(settings => settings.VerificationCodeExpirationMinutes > 0,
                "Email verification code expiration minutes must be greater than 0.")
            .Validate(settings => settings.VerificationCodeResendCooldownMinutes > 0,
                "Email verification code resend cooldown minutes must be greater than 0.")
            .Validate(settings => settings.VerificationCodeResendLimitWindowMinutes > 0,
                "Email verification code resend limit window minutes must be greater than 0.")
            .Validate(settings => settings.VerificationCodeMaxRequestCountInWindow > 0,
                "Email verification code max request count in window must be greater than 0.")
            .Validate(settings => settings.PasswordResetCodeExpirationMinutes > 0,
                "Password reset code expiration minutes must be greater than 0.")
            .Validate(settings => settings.PasswordResetCodeResendCooldownMinutes > 0,
                "Password reset code resend cooldown minutes must be greater than 0.")
            .Validate(settings => settings.PasswordResetCodeResendLimitWindowMinutes > 0,
                "Password reset code resend limit window minutes must be greater than 0.")
            .Validate(settings => settings.PasswordResetCodeMaxRequestCountInWindow > 0,
                "Password reset code max request count in window must be greater than 0.")
            .ValidateOnStart();

        if (environment.IsDevelopment())
        {
            services.AddScoped<IEmailSender, DevelopmentEmailSender>();
        }
        else
        {
            services.AddScoped<IEmailSender, SmtpEmailSender>();
        }

        services.AddScoped<IEmailVerificationService, EmailVerificationService>();
        services.AddScoped<IPasswordResetService, PasswordResetService>();
    }
}