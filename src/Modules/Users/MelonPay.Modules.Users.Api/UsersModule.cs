using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MelonPay.Modules.Users.Core;
using MelonPay.Modules.Users.Core.DTO;
using MelonPay.Modules.Users.Core.Queries;
using MelonPay.Shared.Abstractions.Modules;
using MelonPay.Shared.Infrastructure.Modules;
using MelonPay.Shared.Abstractions.Dispatchers;

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
            app.UseModuleRequests()
                .Subscribe<GetUserByEmail, UserDetailsDto>("users/get",
                    (query, serviceProvider, cancellationToken) =>
                        serviceProvider.GetRequiredService<IDispatcher>().QueryAsync(query, cancellationToken)
                );
        }
    }
}
