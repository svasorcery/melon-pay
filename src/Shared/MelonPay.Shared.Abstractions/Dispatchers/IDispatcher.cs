using MelonPay.Shared.Abstractions.Commands;
using MelonPay.Shared.Abstractions.Queries;

namespace MelonPay.Shared.Abstractions.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default) where TResult : class;
    }
}
