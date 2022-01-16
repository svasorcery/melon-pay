using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Infrastructure.Postgres;
using MelonPay.Modules.Customers.Core.DAL;
using MelonPay.Modules.Customers.Core.DAL.Repositories;
using MelonPay.Modules.Customers.Core.Domain.Repositories;

[assembly: InternalsVisibleTo("MelonPay.Modules.Customers.Api")]
namespace MelonPay.Modules.Customers.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
            => services
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddPostgres<CustomersDbContext>();
    }
}
