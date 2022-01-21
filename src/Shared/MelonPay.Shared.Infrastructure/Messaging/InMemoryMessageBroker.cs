using MelonPay.Shared.Abstractions.Modules;
using MelonPay.Shared.Abstractions.Messaging;
using MelonPay.Shared.Infrastructure.Messaging.Dispatchers;

namespace MelonPay.Shared.Infrastructure.Messaging
{
    internal sealed  class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;
        private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;
        private readonly MessagingOptions _options;

        public InMemoryMessageBroker(IModuleClient moduleClient, IAsyncMessageDispatcher asyncMessageDispatcher, MessagingOptions options)
        {
            _moduleClient = moduleClient;
            _asyncMessageDispatcher = asyncMessageDispatcher;
            _options = options;
        }

        public Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
            => PublishAsync(cancellationToken, message);

        public Task PublishAsync(IMessage[] messages, CancellationToken cancellationToken = default)
            => PublishAsync(cancellationToken, messages);

        private async Task PublishAsync(CancellationToken cancellationToken, params IMessage[] messages)
        {
            if (messages is null)
            {
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();
            if (!messages.Any())
            {
                return;
            }

            var tasks = _options.UseAsyncDispatcher
                ? messages.Select(message => _asyncMessageDispatcher.PublishAsync(message, cancellationToken))
                : messages.Select(message => _moduleClient.PublishAsync(message, cancellationToken));

            await Task.WhenAll(tasks);
        }   
    }
}
