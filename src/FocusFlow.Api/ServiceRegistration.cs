using FluentValidation;
using FocusFlow.Api.Extensions;
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
        services.AddSwaggerServices();
        services.AddJwtAuthentication(configuration);
        services.AddMediatRServices();
        services.AddPersistenceServices(configuration);
    }
}