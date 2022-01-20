using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Shared.Infrastructure;
using MelonPay.Shared.Infrastructure.Postgres;
using MelonPay.Modules.Users.Core.DAL;
using MelonPay.Modules.Users.Core.DAL.Repositories;
using MelonPay.Modules.Users.Core.Domain.Entities;
using MelonPay.Modules.Users.Core.Domain.Repositories;

[assembly: InternalsVisibleTo("MelonPay.Modules.Users.Api")]
namespace MelonPay.Modules.Users.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            var registrationOptions = services.GetOptions<RegistrationOptions>("users:registration");
            services.AddSingleton(registrationOptions);

            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddPostgres<UsersDbContext>();
        
            return services;
        }
    }
}