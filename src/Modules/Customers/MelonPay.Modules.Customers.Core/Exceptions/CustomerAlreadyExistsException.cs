using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Customers.Core.Exceptions
{
    internal class CustomerAlreadyExistsException : MelonPayException
    {
        public Guid CustomerId { get; }

        public CustomerAlreadyExistsException(Guid customerId) : base($"Customer with ID: '{customerId}' already exists.")
        {
            CustomerId = customerId;
        }
    }
}
