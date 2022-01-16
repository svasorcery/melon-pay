using Microsoft.AspNetCore.Mvc;
using MelonPay.Shared.Abstractions.Commands;
using MelonPay.Modules.Customers.Core.Commands;

namespace MelonPay.Modules.Customers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CustomersController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public CustomersController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomer command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
    }
}
