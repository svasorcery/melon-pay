using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace MelonPay.Shared.Infrastructure.Api
{
    internal static class Extensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            var disabledModules = GetDisabledModules(services);

            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.ApplicationParts.RemoveDisabledModules(disabledModules);
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            return services;
        }

        private static List<string> GetDisabledModules(IServiceCollection services)
        {
            var disabledModules = new List<string>();

            using var scope = services.BuildServiceProvider().CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (!key.Contains(":module:enabled"))
                {
                    continue;
                }

                if (!bool.Parse(value))
                {
                    disabledModules.Add(key.Split(":")[0]);
                }
            }

            return disabledModules;
        }

        private static void RemoveDisabledModules(this IList<ApplicationPart> applicationParts, List<string> disabledModules)
        {
            var removeParts = new List<ApplicationPart>();

            foreach (var disabledModule in disabledModules)
            {
                var parts = applicationParts.Where(x => x.Name.Contains(disabledModule));
                removeParts.AddRange(parts);
            }

            foreach (var removePart in removeParts)
            {
                applicationParts.Remove(removePart);
            }
        }
    }
}
