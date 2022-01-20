using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Users.Core.Exceptions
{
    internal class EmailInUseException : MelonPayException
    {
        public EmailInUseException() : base("Email is already in use.")
        {
        }
    }
}
