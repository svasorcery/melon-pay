using MelonPay.Shared.Abstractions.Events;

namespace MelonPay.Modules.Customers.Core.Events.External
{
    internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;
}
