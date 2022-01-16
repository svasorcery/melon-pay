using Microsoft.Extensions.DependencyInjection;

namespace MelonPay.Shared.Infrastructure.Api
{
    internal static class Extensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            return services;
        }
    }
}
