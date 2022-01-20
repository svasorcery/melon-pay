using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MelonPay.Shared.Infrastructure.Api;
using MelonPay.Shared.Abstractions.Dispatchers;
using MelonPay.Modules.Users.Core.Commands;
using MelonPay.Modules.Users.Core.Queries;

namespace MelonPay.Modules.Users.Api.Controllers
{
    internal class AccountController : ApiControllerBase
    {
        public AccountController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var userId = Guid.TryParse(HttpContext.User?.Identity?.Name, out Guid guid) ? guid : Guid.Empty;
            return SingleOrNotFound(await Dispatcher.QueryAsync(new GetUser { UserId = userId }));
        }

       [HttpPost("sign-up")]
        public async Task<ActionResult> SignUpAsync(SignUp command)
        {
            await Dispatcher.SendAsync(command, CancellationToken);
            return NoContent();
        }
    }
}
