using MelonPay.Shared.Abstractions.Messaging;
using MelonPay.Shared.Infrastructure.Messaging.Channels;

namespace MelonPay.Shared.Infrastructure.Messaging.Dispatchers
{
    internal class AsyncMessageDispatcher : IAsyncMessageDispatcher
    {
        private readonly IMessageChannel _messageChannel;

        public AsyncMessageDispatcher(IMessageChannel messageChannel)
        {
            _messageChannel = messageChannel;
        }

        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class, IMessage
        {
            await _messageChannel.Writer.WriteAsync(new MessageEnvelope(message), cancellationToken);
        }
    }
}
