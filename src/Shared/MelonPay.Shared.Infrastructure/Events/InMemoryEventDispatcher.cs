using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Events;

namespace MelonPay.Shared.Infrastructure.Events
{
    internal sealed class InMemoryEventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
            var tasks = handlers.Select(x => x.HandleAsync(@event, cancellationToken));
            await Task.WhenAll(tasks);
        }
    }
}
