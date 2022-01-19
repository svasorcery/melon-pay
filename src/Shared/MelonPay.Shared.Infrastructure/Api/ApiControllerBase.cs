using Microsoft.AspNetCore.Mvc;
using MelonPay.Shared.Abstractions.Dispatchers;
using MelonPay.Shared.Abstractions.Queries.Pagination;

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

        protected ActionResult<Paged<T>> PagedOrNoContent<T>(Paged<T>? paged)
            => paged is not null && !paged.IsEmpty
                ? Ok(paged)
                : NoContent();

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
