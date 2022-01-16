using MelonPay.Shared.Infrastructure;
using MelonPay.Modules.Customers.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomersModule()
    .AddModularInfrastructure();

var app = builder.Build();

app
    .UseModularInfrastructure()
    .UseCustomersModule();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "MelonPay API");

app.Run();
