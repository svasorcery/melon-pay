using System.Threading.Channels;

namespace MelonPay.Shared.Infrastructure.Messaging.Channels
{
    internal class MessageChannel : IMessageChannel
    {
        private readonly Channel<MessageEnvelope> _messages = Channel.CreateUnbounded<MessageEnvelope>();

        public ChannelReader<MessageEnvelope> Reader => _messages.Reader;
        public ChannelWriter<MessageEnvelope> Writer => _messages.Writer;
    }
}
