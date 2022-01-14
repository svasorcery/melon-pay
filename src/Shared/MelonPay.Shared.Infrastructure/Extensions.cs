using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MelonPay.Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services)
            => services;

        public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app)
            => app;
    }
}