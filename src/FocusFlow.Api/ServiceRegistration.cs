using FluentValidation;
using FocusFlow.Api.Features.Auth.Rules;
using FocusFlow.Api.Infrastructure.Security;
using FocusFlow.Api.Infrastructure.Token;
using FocusFlow.Api.Persistence.Context;
using FocusFlow.Api.Shared.Abstractions.Security;
using FocusFlow.Api.Shared.Abstractions.Token;
using FocusFlow.Api.Shared.Behaviors;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace FocusFlow.Api;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddAuthorization();

        var tokenSettings = configuration
            .GetSection("JWT")
            .Get<TokenSettings>()
            ?? throw new InvalidOperationException("JWT settings missing.");

        services.AddSingleton(tokenSettings);

        #region Swagger
        services.AddSwaggerGen(gen =>
        { 
            gen.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FocusFlow Web Api",
                Version = "v1",
                License = new OpenApiLicense
                {
                    Name = "Powered by Berkay Uludoğan", 
                },
                Contact = new OpenApiContact
                {
                    Name = "Berkay Uludoğan",
                    Email = "buludogan0@gmail.com"
                }
            });

            gen.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization: Bearer {token}"
            });
            gen.AddSecurityRequirement(doc =>

                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", doc)] = new List<string>()
                });
        });
        var key = configuration["JWT:SecurityKey"]
                           ?? throw new InvalidOperationException("JWT:SecurityKey missing");
        // TODO: Exceptionu düzelt.
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {

                opt.TokenValidationParameters = new()
                {
                    ValidateAudience = true, // Oluşturulacak token değerinin kimlerin/hangi originlerin/sitelerin kullanıcı belirlediğimiz değerdir.
                    ValidateIssuer = true, // Oluşturulacak token değerinin kimin dağıttığını ifade edeceğimiz alan
                    ValidateLifetime = true, // Oluşturulan token değerinin süresini kontrol edecek olan doğrulama
                    ValidateIssuerSigningKey = true, // Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden suciry key verisinin doğrulamasıdır.

                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                    expires != null ? expires > DateTime.UtcNow : false,
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.Name //JWT üzerinde Name claimne karşılık gelen değeri User.Identity.Name propertysinden elde edebiliriz.
                };
            });

        #endregion
    
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);


        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IAuthBusinessRules, AuthBusinessRules>();
        services.AddScoped<ITokenService, TokenService>();  

        services.AddDbContext<FocusFlowDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
}