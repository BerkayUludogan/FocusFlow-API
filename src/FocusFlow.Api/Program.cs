using FocusFlow.Api;
using FocusFlow.Api.Features.Auth.Register;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "FocusFlow API");

app.MapRegisterEndpoint();

app.Run();  