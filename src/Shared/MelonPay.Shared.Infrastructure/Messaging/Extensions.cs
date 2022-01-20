using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Messaging;

namespace MelonPay.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
            => services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
    }
}
