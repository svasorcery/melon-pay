using MelonPay.Shared.Abstractions.Messaging;

namespace MelonPay.Shared.Infrastructure.Messaging.Dispatchers
{
    internal interface IAsyncMessageDispatcher
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class, IMessage;
    }
}
