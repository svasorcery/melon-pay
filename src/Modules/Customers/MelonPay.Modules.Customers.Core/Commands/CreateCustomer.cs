using MelonPay.Shared.Abstractions.Commands;

namespace MelonPay.Modules.Customers.Core.Commands
{
    internal record CreateCustomer(string Email) : ICommand;
}
