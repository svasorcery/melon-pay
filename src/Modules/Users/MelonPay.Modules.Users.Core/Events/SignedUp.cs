using MelonPay.Shared.Abstractions.Events;

namespace MelonPay.Modules.Users.Core.Events
{
    internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;
}
