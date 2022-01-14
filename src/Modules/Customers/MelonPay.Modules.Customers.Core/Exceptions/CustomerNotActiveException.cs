using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Customers.Core.Exceptions
{
    internal class CustomerNotActiveException : MelonPayException
    {
        public Guid CustomerId { get; set; }

        public CustomerNotActiveException(Guid customerId) : base($"Customer with ID: '{customerId}' is not active.")
        {
            CustomerId = customerId;
        }
    }
}
