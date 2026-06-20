using FocusFlow.Api;
using FocusFlow.Api.Shared.Middleware;
using FocusFlow.Api.Features.Auth.Register;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);




var app = builder.Build();
#region Middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();
#endregion
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseAuthentication();
app.UseAuthorization();

app.MapRegisterEndpoint();

app.Run();  