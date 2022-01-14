using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Customers.Core.Exceptions
{
    internal class InvalidNameException : MelonPayException
    {
        public string Name { get; }

        public InvalidNameException(string name) : base($"Name: '{name}' is invalid.")
        {
            Name = name;
        }
    }
}
