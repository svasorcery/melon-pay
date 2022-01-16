using Microsoft.EntityFrameworkCore;
using MelonPay.Modules.Customers.Core.Domain.Entities;

namespace MelonPay.Modules.Customers.Core.DAL
{
    internal class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("customers");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
