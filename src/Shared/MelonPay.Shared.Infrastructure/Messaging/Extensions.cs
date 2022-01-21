using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Messaging;
using MelonPay.Shared.Infrastructure.Messaging.Channels;

namespace MelonPay.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
            => services
                .AddSingleton<IMessageChannel, MessageChannel>()
                .AddTransient<IMessageBroker, InMemoryMessageBroker>();
    }
}
