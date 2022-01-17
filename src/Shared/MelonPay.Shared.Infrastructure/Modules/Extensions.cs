using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

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
    }
}
