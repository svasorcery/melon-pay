using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Modules;
using MelonPay.Shared.Infrastructure.Modules.Requests;
using MelonPay.Shared.Infrastructure.Modules.Registry;

namespace MelonPay.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        public static IHostBuilder ConfigureModules(this IHostBuilder builder)
            => builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach (var settings in GetSettings("*"))
                {
                    cfg.AddJsonFile(settings);
                }

                IEnumerable<string> GetSettings(string pattern)
                    => Directory.EnumerateFiles(
                        ctx.HostingEnvironment.ContentRootPath,
                        $"modules.{pattern}.json",
                        SearchOption.AllDirectories);
            });

        internal static IServiceCollection AddModuleRequests(this IServiceCollection services)
            => services
                .AddSingleton<IModuleRegistry, ModuleRegistry>()
                .AddSingleton<IModuleSubscriber, ModuleSubscriber>()
                .AddSingleton<IModuleSerializer, JsonModuleSerializer>()
                .AddSingleton<IModuleClient, ModuleClient>();

        public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
            => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();
    }
}
