using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Users.Core.Exceptions
{
    internal class InvalidEmailException : MelonPayException
    {
        public string Email { get; }

        public InvalidEmailException(string email) : base($"Email is invalid: '{email}'.")
        {
            Email = email;
        }
    }
}
