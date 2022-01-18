using MelonPay.Modules.Users.Core.Domain.Entities;

namespace MelonPay.Modules.Users.Core.Domain.Repositories
{
    internal interface IRoleRepository
    {
        Task<IReadOnlyList<Role>> GetAllAsync();
        Task<Role> GetAsync(string name);
        Task AddAsync(Role role);
    }
}
