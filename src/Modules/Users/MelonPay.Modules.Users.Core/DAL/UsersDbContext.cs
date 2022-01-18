using Microsoft.EntityFrameworkCore;
using MelonPay.Modules.Users.Core.Domain.Entities;

namespace MelonPay.Modules.Users.Core.DAL
{
    internal class UsersDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
