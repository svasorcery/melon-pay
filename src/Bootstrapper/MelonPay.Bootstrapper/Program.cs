using MelonPay.Shared.Infrastructure;
using MelonPay.Modules.Customers.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddModularInfrastructure()
    .AddCustomersModule();

var app = builder.Build();

app
    .UseModularInfrastructure()
    .UseCustomersModule();

app.MapGet("/", () => "MelonPay API");

app.Run();
