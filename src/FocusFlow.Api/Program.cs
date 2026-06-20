using FocusFlow.Api;
using FocusFlow.Api.Features.Auth.Register;
using FocusFlow.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapGet("/", () => "FocusFlow API");

app.MapRegisterEndpoint();

app.Run();  