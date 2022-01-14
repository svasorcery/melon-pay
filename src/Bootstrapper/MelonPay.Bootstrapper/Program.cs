using MelonPay.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddModularInfrastructure();

var app = builder.Build();

app
    .UseModularInfrastructure();

app.MapGet("/", () => "MelonPay API");

app.Run();
