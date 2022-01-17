using Microsoft.AspNetCore.Mvc;
using MelonPay.Shared.Abstractions.Dispatchers;

namespace MelonPay.Shared.Infrastructure.Api
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly IDispatcher Dispatcher;
		protected CancellationToken CancellationToken => HttpContext.RequestAborted;

        protected ApiControllerBase(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        protected IActionResult SingleOrNotFound<T>(T? model)
            => model is not null
                ? Ok(model)
                : NotFound();

        protected IActionResult ManyOrNoContent<T>(IEnumerable<T>? items)
            => items is not null && items.Any()
                ? Ok(items)
                : NoContent();
    }
}
