using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Customers.Core.Exceptions
{
    internal class InvalidAddressException : MelonPayException
    {
        public string Address { get; }

        public InvalidAddressException(string address) : base($"Address: '{address}' is invalid.")
        {
            Address = address;
        }
    }
}
