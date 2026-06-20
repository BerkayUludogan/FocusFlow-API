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
        var tokenSettings = configuration
            .GetSection("JWT")
            .Get<TokenSettings>()
            ?? throw new InvalidOperationException("JWT settings missing.");

        if (string.IsNullOrWhiteSpace(tokenSettings.SecurityKey))
        {
            throw new InvalidOperationException("JWT:SecurityKey missing.");
        }

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