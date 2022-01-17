using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Abstractions.Queries;

namespace MelonPay.Shared.Infrastructure.Queries
{
    internal sealed class InMemoryQueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryQueryDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default) where TResult : class
        {
            using var scope = _serviceProvider.CreateScope();
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            var method = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));
            if (method is null)
            {
                throw new InvalidOperationException($"Query handler for '{typeof(TResult).Name}' is invalid.");
            }

            // ReSharper disable once PossibleNullReferenceException
            return await (Task<TResult>)method.Invoke(handler, new object[] { query, cancellationToken });
        }
    }
}
