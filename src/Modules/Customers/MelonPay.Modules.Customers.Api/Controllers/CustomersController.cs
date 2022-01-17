using Microsoft.AspNetCore.Mvc;
using MelonPay.Shared.Abstractions.Dispatchers;
using MelonPay.Modules.Customers.Core.Commands;
using MelonPay.Modules.Customers.Core.Queries;

namespace MelonPay.Modules.Customers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CustomersController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public CustomersController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> Get([FromRoute] GetCustomer query)
        {
            var customer = await _dispatcher.QueryAsync(query);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomer command)
        {
            await _dispatcher.SendAsync(command);
            return Accepted();
        }
    }
}
