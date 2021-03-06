using MelonPay.Shared.Abstractions.Dispatchers;
using MelonPay.Shared.Abstractions.Commands;
using MelonPay.Shared.Abstractions.Events;
using MelonPay.Shared.Abstractions.Queries;

namespace MelonPay.Shared.Infrastructure.Dispatchers
{
    internal sealed class InMemoryDispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public InMemoryDispatcher(ICommandDispatcher commandDispatcher, IEventDispatcher eventDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _eventDispatcher = eventDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : class, ICommand
            => _commandDispatcher.SendAsync(command, cancellationToken);

        public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : class, IEvent
            => _eventDispatcher.PublishAsync(@event, cancellationToken);

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
            where TResult : class
            => _queryDispatcher.QueryAsync(query, cancellationToken);
    }
}
