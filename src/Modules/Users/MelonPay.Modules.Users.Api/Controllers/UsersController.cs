using Microsoft.AspNetCore.Mvc;
using MelonPay.Shared.Infrastructure.Api;
using MelonPay.Shared.Abstractions.Dispatchers;
using MelonPay.Shared.Abstractions.Queries.Pagination;
using MelonPay.Modules.Users.Core.DTO;
using MelonPay.Modules.Users.Core.Queries;

namespace MelonPay.Modules.Users.Api.Controllers
{
    internal class UsersController : ApiControllerBase
    {
        public UsersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<Paged<UserDto>>> BrowseAsync([FromQuery] BrowseUsers query)
            => PagedOrNoContent(await Dispatcher.QueryAsync(query, CancellationToken));

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> Get([FromRoute] GetUser query)
            => SingleOrNotFound(await Dispatcher.QueryAsync(query, CancellationToken));

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail([FromRoute] GetUserByEmail query)
            => SingleOrNotFound(await Dispatcher.QueryAsync(query, CancellationToken));
    }
}
