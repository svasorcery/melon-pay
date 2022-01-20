using MelonPay.Shared.Abstractions.Commands;

namespace MelonPay.Modules.Users.Core.Commands
{
    internal record SignUp(string Email, string Password, string Role) : ICommand
    {
        public Guid UserId { get; init; } = Guid.NewGuid();
    }
}
