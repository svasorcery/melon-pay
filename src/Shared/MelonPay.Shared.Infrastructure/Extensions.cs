using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Time;
using MelonPay.Shared.Infrastructure.Time;
using MelonPay.Shared.Infrastructure.Commands;

[assembly: InternalsVisibleTo("MelonPay.Bootstrapper")]
namespace MelonPay.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services)
            => services
                .AddCommands()
                .AddSingleton<IClock, UtcClock>();

        public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app)
            => app;
    }
}