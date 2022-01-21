using MelonPay.Shared.Abstractions.Events;
using MelonPay.Shared.Abstractions.Contracts;
using MelonPay.Shared.Abstractions.Messaging;

namespace MelonPay.Modules.Customers.Core.Events.External
{
    internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;

    [Message("users")]
    internal class SignedUpContract : Contract<SignedUp>
    {
        public SignedUpContract() : base("users")
        {
            RequireAll();
        }
    }
}
