using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Time;
using MelonPay.Shared.Infrastructure.Api;
using MelonPay.Shared.Infrastructure.Time;
using MelonPay.Shared.Infrastructure.Modules;
using MelonPay.Shared.Infrastructure.Postgres;
using MelonPay.Shared.Infrastructure.Commands;
using MelonPay.Shared.Infrastructure.Events;
using MelonPay.Shared.Infrastructure.Queries;
using MelonPay.Shared.Infrastructure.Dispatchers;

[assembly: InternalsVisibleTo("MelonPay.Bootstrapper")]
namespace MelonPay.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IList<Assembly> assemblies)
            => services
                .AddCommands(assemblies)
                .AddEvents(assemblies)
                .AddQueries(assemblies)
                .AddDispatchers()
                .AddPostgres()
                .AddSingleton<IClock, UtcClock>()
                .AddModuleRequests()
                .AddApi();

        public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app)
            => app;
    }
}