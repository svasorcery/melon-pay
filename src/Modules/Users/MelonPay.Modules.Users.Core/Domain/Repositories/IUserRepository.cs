using MelonPay.Modules.Users.Core.Domain.Entities;

namespace MelonPay.Modules.Users.Core.Domain.Repositories
{
    internal interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
