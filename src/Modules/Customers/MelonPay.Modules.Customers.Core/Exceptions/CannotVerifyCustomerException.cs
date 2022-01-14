using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Customers.Core.Exceptions
{
    internal class CannotVerifyCustomerException : MelonPayException
    {
        public Guid CustomerId { get; set; }

        public CannotVerifyCustomerException(Guid customerId) : base($"Customer with ID: '{customerId}' cannot be verified.")
        {
            CustomerId = customerId;
        }
    }
}
