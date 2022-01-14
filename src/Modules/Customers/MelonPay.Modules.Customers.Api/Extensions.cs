using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Modules.Customers.Core;

[assembly: InternalsVisibleTo("MelonPay.Bootstrapper")]
namespace MelonPay.Modules.Customers.Api
{
    internal static class Extensions
    {
        public static IServiceCollection AddCustomersModule(this IServiceCollection services)
            => services
                .AddCore();

        public static IApplicationBuilder UseCustomersModule(this IApplicationBuilder app)
            => app;
    }
}
