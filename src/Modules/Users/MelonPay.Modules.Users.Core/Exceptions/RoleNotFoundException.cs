using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Modules.Users.Core.Exceptions
{
    internal class RoleNotFoundException : MelonPayException
    {
        public string Role { get; }

        public RoleNotFoundException(string role) : base($"Role: '{role}' was not found.")
        {
            Role = role;
        }
    }
}
