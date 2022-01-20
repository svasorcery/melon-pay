using MelonPay.Shared.Abstractions.Messaging;
using MelonPay.Shared.Abstractions.Modules;

namespace MelonPay.Shared.Infrastructure.Messaging
{
    internal sealed  class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;

        public InMemoryMessageBroker(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
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

            var tasks = messages.Select(message => _moduleClient.PublishAsync(message, cancellationToken));
            await Task.WhenAll(tasks);
        }   
    }
}
