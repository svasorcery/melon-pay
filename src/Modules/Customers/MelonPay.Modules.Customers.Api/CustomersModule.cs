using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Modules.Customers.Core;
using MelonPay.Shared.Abstractions.Modules;

[assembly: InternalsVisibleTo("MelonPay.Bootstrapper")]
namespace MelonPay.Modules.Customers.Api
{
    internal class CustomersModule : IModule
    {
        public string Name => "Customers";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
