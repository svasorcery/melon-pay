using Microsoft.AspNetCore.Mvc;
using MelonPay.Shared.Infrastructure.Api;
using MelonPay.Shared.Abstractions.Dispatchers;
using MelonPay.Modules.Customers.Core.Commands;
using MelonPay.Modules.Customers.Core.Queries;

namespace MelonPay.Modules.Customers.Api.Controllers
{
    internal class CustomersController : ApiControllerBase
    {
        public CustomersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> Get([FromRoute] GetCustomer query)
            => SingleOrNotFound(await Dispatcher.QueryAsync(query, CancellationToken));

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomer command)
        {
            await Dispatcher.SendAsync(command, CancellationToken);
            return Accepted();
        }
    }
}
