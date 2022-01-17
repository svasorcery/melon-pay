using Microsoft.AspNetCore.Mvc;
using MelonPay.Shared.Abstractions.Commands;
using MelonPay.Shared.Abstractions.Queries;
using MelonPay.Modules.Customers.Core.Commands;
using MelonPay.Modules.Customers.Core.Queries;

namespace MelonPay.Modules.Customers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CustomersController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public CustomersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> Get([FromRoute] GetCustomer query)
        {
            var customer = await _queryDispatcher.QueryAsync(query);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomer command)
        {
            await _commandDispatcher.SendAsync(command);
            return Accepted();
        }
    }
}
