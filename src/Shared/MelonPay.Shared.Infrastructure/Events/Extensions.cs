using System.Reflection;
using MelonPay.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace MelonPay.Shared.Infrastructure.Events
{
    internal static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services, IList<Assembly> assemblies)
        {
            services.AddSingleton<IEventDispatcher, InMemoryEventDispatcher>();

            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
