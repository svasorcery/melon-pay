using MelonPay.Shared.Infrastructure;
using MelonPay.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;
var services = builder.Services;

var assemblies = ModuleLoader.LoadAssemblies(configuration);
var modules = ModuleLoader.LoadModules(assemblies);

services.AddModularInfrastructure(assemblies);
foreach (var module in modules)
{
    module.Register(services);
}

var app = builder.Build();

app.UseModularInfrastructure();
app.UseHttpsRedirection();
app.UseRouting();
foreach (var module in modules)
{
    module.Use(app);
}
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "MelonPay API");

assemblies.Clear();
modules.Clear();

app.Run();
