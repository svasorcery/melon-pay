using MelonPay.Shared.Abstractions.Queries;
using MelonPay.Modules.Users.Core.DTO;

namespace MelonPay.Modules.Users.Core.Queries
{
    internal class GetUserByEmail : IQuery<UserDetailsDto>
    {
        public string Email { get; set; }
    }
}
