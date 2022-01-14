using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Customers.Core.Exceptions
{
    internal class InvalidIdentityException : MelonPayException
    {
        public string Type { get; }
        public string Series { get; }

        public InvalidIdentityException(string type, string series)
            : base($"Identity type: '{type}', series: '{series}' is invalid.")
        {
            Type = type;
            Series = series;
        }
    }
}
