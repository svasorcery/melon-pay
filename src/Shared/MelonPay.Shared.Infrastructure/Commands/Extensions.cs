using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Commands;

namespace MelonPay.Shared.Infrastructure.Commands
{
    internal static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services, IList<Assembly> assemblies)
        {
            services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();

            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
