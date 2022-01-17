using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Dispatchers;

namespace MelonPay.Shared.Infrastructure.Dispatchers
{
    internal static class Extensions
    {
        public static IServiceCollection AddDispatchers(this IServiceCollection services)
            => services.AddSingleton<IDispatcher, InMemoryDispatcher>();
    }
}
