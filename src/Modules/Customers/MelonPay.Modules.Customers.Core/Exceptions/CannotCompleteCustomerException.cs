using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Customers.Core.Exceptions
{
    internal class CannotCompleteCustomerException : MelonPayException
    {
        public Guid CustomerId { get; set; }

        public CannotCompleteCustomerException(Guid customerId) : base($"Customer with ID: '{customerId}' cannot be complated.")
        {
            CustomerId = customerId;
        }
    }
}
