using MelonPay.Shared.Abstractions.Modules;
using MelonPay.Modules.Customers.Core.Clients.DTO;

namespace MelonPay.Modules.Customers.Core.Clients
{
    internal sealed class UserApiClient : IUserApiClient
    {
        private readonly IModuleClient _moduleClient;

        public UserApiClient(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }

        public Task<UserDto> GetAsync(string email, CancellationToken cancellationToken = default)
            => _moduleClient.SendAsync<UserDto>("users/get", new { email }, cancellationToken);
    }
}
