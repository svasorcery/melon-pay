using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Infrastructure.Modules.Registry;

namespace MelonPay.Shared.Infrastructure.Modules
{
    internal static class Extensions
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
                .AddSingleton<IModuleRegistry, ModuleRegistry>();
    }
}
