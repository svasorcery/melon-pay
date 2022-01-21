using System.Threading.Channels;

namespace MelonPay.Shared.Infrastructure.Messaging.Channels
{
    internal interface IMessageChannel
    {
        ChannelReader<MessageEnvelope> Reader { get; }
        ChannelWriter<MessageEnvelope> Writer { get; }
    }
}
