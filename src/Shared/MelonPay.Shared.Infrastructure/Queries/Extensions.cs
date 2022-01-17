using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Queries;

namespace MelonPay.Shared.Infrastructure.Queries
{
    internal static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services, IList<Assembly> assemblies)
        {
            services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();

            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
