using MelonPay.Modules.Users.Core.DTO;
using MelonPay.Shared.Abstractions.Queries.Pagination;

namespace MelonPay.Modules.Users.Core.Queries
{
    internal class BrowseUsers : PagedQuery<UserDto>
    {
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? State { get; set; }
    }
}
