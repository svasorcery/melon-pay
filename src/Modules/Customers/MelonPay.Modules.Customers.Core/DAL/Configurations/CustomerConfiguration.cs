using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MelonPay.Modules.Customers.Core.Domain.Entities;
using MelonPay.Modules.Customers.Core.Domain.ValueObjects;

namespace MelonPay.Modules.Customers.Core.DAL.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .HasConversion(x => x.Value, x => new(x));

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .HasConversion(x => x.Value, x => new(x));

            builder.HasIndex(x => x.FullName);
            builder.Property(x => x.FullName)
                .HasMaxLength(100)
                .HasConversion(x => x.Value, x => new(x));

            builder.HasIndex(x => x.Address);
            builder.Property(x => x.Address)
                .HasMaxLength(200)
                .HasConversion(x => x.Value, x => new(x));

            builder.HasIndex(x => x.Identity);
            builder.Property(x => x.Identity)
                .HasMaxLength(50)
                .HasConversion(x => x.ToString(), x => Identity.From(x));

            builder.HasIndex(x => x.Nationality);
            builder.Property(x => x.Nationality)
                .HasMaxLength(2)
                .HasConversion(x => x.Value, x => new(x));

            builder.HasIndex(x => x.Notes);
            builder.Property(x => x.Notes)
                .HasMaxLength(500);
        }
    }
}
