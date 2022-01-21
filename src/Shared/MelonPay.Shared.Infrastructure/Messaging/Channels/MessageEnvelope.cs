using MelonPay.Shared.Abstractions.Messaging;

namespace MelonPay.Shared.Infrastructure.Messaging.Channels
{
    internal record MessageEnvelope(IMessage Message);
}
