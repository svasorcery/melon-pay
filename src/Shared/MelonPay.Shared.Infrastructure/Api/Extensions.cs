using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Infrastructure.Modules;

namespace MelonPay.Shared.Infrastructure.Api
{
    internal static class Extensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            var disabledModules = services.GetDisabledModules();

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
    }
}
