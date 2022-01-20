using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Users.Core.Exceptions
{
    internal class InvalidPasswordException : MelonPayException
    {
        public string Reason { get; }

        public InvalidPasswordException(string reason) : base($"Invalid password: {reason}.")
        {
            Reason = reason;
        }
    }
}
