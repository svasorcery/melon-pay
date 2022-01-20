using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Commands;
using MelonPay.Shared.Abstractions.Events;
using MelonPay.Shared.Abstractions.Modules;
using MelonPay.Shared.Infrastructure.Modules.Requests;
using MelonPay.Shared.Infrastructure.Modules.Registry;

namespace MelonPay.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        public static IHostBuilder ConfigureModules(this IHostBuilder builder)
            => builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach (var settings in GetSettings("*"))
                {
                    cfg.AddJsonFile(settings);
                }

                IEnumerable<string> GetSettings(string pattern)
                    => Directory.EnumerateFiles(
                        ctx.HostingEnvironment.ContentRootPath,
                        $"modules.{pattern}.json",
                        SearchOption.AllDirectories);
            });

        internal static IServiceCollection AddModuleRequests(this IServiceCollection services, IList<Assembly> assemblies)
        {
            services.AddModuleRegistry(assemblies);

            services
                .AddSingleton<IModuleSubscriber, ModuleSubscriber>()
                .AddSingleton<IModuleSerializer, JsonModuleSerializer>()
                .AddSingleton<IModuleClient, ModuleClient>();

            return services;
        }

        private static void AddModuleRegistry(this IServiceCollection services, IList<Assembly> assemblies)
        {
            var registry = new ModuleRegistry();
            var types = assemblies
                .SelectMany(x => x.GetTypes())
                .ToArray();

            var commandTypes = types
                .Where(t => t.IsClass && typeof(ICommand).IsAssignableFrom(t))
                .ToArray();

            var eventTypes = types
                .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
                .ToArray();

            services.AddSingleton<IModuleRegistry>(sp =>
            {
                var commandDispatcher = sp.GetRequiredService<ICommandDispatcher>();
                var commandDispatcherType = commandDispatcher.GetType();

                foreach (var type in commandTypes)
                {
                    var registration = new ModuleBroadcastRegistration(type, (@event, cancellationToken) =>
                        (Task)commandDispatcherType.GetMethod(nameof(commandDispatcher.SendAsync))
                            ?.MakeGenericMethod(type)
                            .Invoke(commandDispatcher, new[] { @event, cancellationToken }));

                    registry.AddBroadcastAction(registration);
                }

                var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
                var eventDispatcherType = eventDispatcher.GetType();

                foreach (var type in eventTypes)
                {
                    var registration = new ModuleBroadcastRegistration(type, (@event, cancellationToken) =>
                        (Task)eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))
                            ?.MakeGenericMethod(type)
                            .Invoke(eventDispatcher, new[] { @event, cancellationToken }));

                    registry.AddBroadcastAction(registration);
                }

                return registry;
            });
        }

        public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
            => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();
    }
}
