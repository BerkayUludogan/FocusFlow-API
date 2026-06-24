using System.Security.Claims;
using System.Text;
using FocusFlow.Api.Infrastructure.Token;
using FocusFlow.Api.Shared.Abstractions.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FocusFlow.Api.Extensions;

public static class AuthenticationServiceExtensions
{
    public static void AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<TokenSettings>()
        .Bind(configuration.GetSection("JWT"))
        .Validate(settings => !string.IsNullOrWhiteSpace(settings.Audience),
            "JWT audience must not be empty.")
        .Validate(settings => !string.IsNullOrWhiteSpace(settings.Issuer),
            "JWT issuer must not be empty.")
        .Validate(settings => !string.IsNullOrWhiteSpace(settings.SecurityKey),
            "JWT security key must not be empty.")
        .Validate(settings => settings.TokenExpirationInMinutes > 0,
            "JWT token expiration minutes must be greater than 0.")
        .Validate(settings => settings.RefreshTokenExpirationInDays > 0,
            "JWT refresh token expiration days must be greater than 0.")
        .ValidateOnStart();

        var tokenSettings = configuration
                .GetSection("JWT")
                .Get<TokenSettings>()
                ?? throw new InvalidOperationException("JWT settings missing.");

        services.AddSingleton(tokenSettings);
        services.AddScoped<ITokenService, TokenService>();

        services.AddAuthorization();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidAudience = tokenSettings.Audience,
                ValidIssuer = tokenSettings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(tokenSettings.SecurityKey)),

                LifetimeValidator = (_, expires, _, _) =>
                    expires is not null && expires > DateTime.UtcNow,

                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name
            };
        });
    }
}