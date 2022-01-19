using MelonPay.Modules.Customers.Core.Clients.DTO;

namespace MelonPay.Modules.Customers.Core.Clients
{
    internal interface IUserApiClient
    {
        Task<UserDto> GetAsync(string email, CancellationToken cancellationToken = default);
    }
}
