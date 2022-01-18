using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Modules.Users.Core;
using MelonPay.Shared.Abstractions.Modules;

[assembly: InternalsVisibleTo("MelonPay.Bootstrapper")]
namespace MelonPay.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public string Name => "Users";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
