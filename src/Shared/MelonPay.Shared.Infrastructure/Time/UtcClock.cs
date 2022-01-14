using MelonPay.Shared.Abstractions.Time;

namespace MelonPay.Shared.Infrastructure.Time
{
    internal class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
    }
}
