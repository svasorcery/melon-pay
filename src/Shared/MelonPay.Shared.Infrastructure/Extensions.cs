using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Time;
using MelonPay.Shared.Infrastructure.Api;
using MelonPay.Shared.Infrastructure.Time;
using MelonPay.Shared.Infrastructure.Modules;
using MelonPay.Shared.Infrastructure.Postgres;
using MelonPay.Shared.Infrastructure.Commands;
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
                .AddQueries(assemblies)
                .AddDispatchers()
                .AddPostgres()
                .AddSingleton<IClock, UtcClock>()
                .AddModuleRequests()
                .AddApi();

        public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app)
            => app;

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : class, new()
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }
    }
}