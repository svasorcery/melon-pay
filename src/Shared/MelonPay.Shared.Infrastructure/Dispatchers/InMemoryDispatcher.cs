using MelonPay.Shared.Abstractions.Dispatchers;
using MelonPay.Shared.Abstractions.Commands;
using MelonPay.Shared.Abstractions.Queries;

namespace MelonPay.Shared.Infrastructure.Dispatchers
{
    internal sealed class InMemoryDispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public InMemoryDispatcher(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        Task IDispatcher.SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
            => _commandDispatcher.SendAsync(command, cancellationToken);

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default) where TResult : class
            => _queryDispatcher.QueryAsync(query, cancellationToken);
    }
}
