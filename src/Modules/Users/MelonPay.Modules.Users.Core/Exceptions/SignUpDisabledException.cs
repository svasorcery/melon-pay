using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Users.Core.Exceptions
{
    internal class SignUpDisabledException : MelonPayException
    {
        public SignUpDisabledException() : base("Sign up is disabled.")
        {
        }
    }
}
