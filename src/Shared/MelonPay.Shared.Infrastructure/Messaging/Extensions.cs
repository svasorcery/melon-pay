using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Messaging;
using MelonPay.Shared.Infrastructure.Messaging.Channels;
using MelonPay.Shared.Infrastructure.Messaging.Dispatchers;

namespace MelonPay.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            var messagingOptions = services.GetOptions<MessagingOptions>("messaging");

            services
                .AddSingleton(messagingOptions)
                .AddSingleton<IMessageChannel, MessageChannel>()
                .AddTransient<IMessageBroker, InMemoryMessageBroker>()
                .AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();

            if (messagingOptions.UseAsyncDispatcher)
            {
                services.AddHostedService<AsyncDispatcherJob>();
            }

            return services;
        }
    }
}
