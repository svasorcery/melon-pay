using Microsoft.EntityFrameworkCore;
using MelonPay.Modules.Customers.Core.Domain.Entities;
using MelonPay.Modules.Customers.Core.Domain.Repositories;

namespace MelonPay.Modules.Customers.Core.DAL.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersDbContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(CustomersDbContext context)
        {
            _context = context;
            _customers = _context.Customers;
        }

        public Task<bool> ExistsAsync(string name)
            => _customers.AnyAsync(x => x.Name == name);

        public Task<Customer> GetAsync(Guid id)
            => _customers.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Customer customer)
        {
            await _customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
