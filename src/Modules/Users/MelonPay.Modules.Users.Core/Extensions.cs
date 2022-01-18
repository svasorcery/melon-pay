using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Infrastructure.Postgres;
using MelonPay.Modules.Users.Core.DAL;
using MelonPay.Modules.Users.Core.DAL.Repositories;
using MelonPay.Modules.Users.Core.Domain.Repositories;

[assembly: InternalsVisibleTo("MelonPay.Modules.Users.Api")]
namespace MelonPay.Modules.Users.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
            => services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddPostgres<UsersDbContext>();
    }
}