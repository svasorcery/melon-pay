using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MelonPay.Shared.Abstractions.Modules;
using MelonPay.Shared.Infrastructure.Messaging.Channels;

namespace MelonPay.Shared.Infrastructure.Messaging
{
    internal sealed class AsyncDispatcherJob : BackgroundService
    {
        private readonly IMessageChannel _messageChannel;
        private readonly IModuleClient _moduleClient;
        private readonly ILogger<AsyncDispatcherJob> _logger;

        public AsyncDispatcherJob(IMessageChannel messageChannel, IModuleClient moduleClient, ILogger<AsyncDispatcherJob> logger)
        {
            _messageChannel = messageChannel;
            _moduleClient = moduleClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var envelope in _messageChannel.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await _moduleClient.PublishAsync(envelope.Message, stoppingToken);
                }
                catch (Exception exception)
                {

                    _logger.LogError(exception, exception.Message);
                }
            }
        }
    }
}
