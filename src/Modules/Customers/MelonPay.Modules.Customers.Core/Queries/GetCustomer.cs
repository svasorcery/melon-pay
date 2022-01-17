using MelonPay.Shared.Abstractions.Queries;
using MelonPay.Modules.Customers.Core.DTO;

namespace MelonPay.Modules.Customers.Core.Queries
{
    internal class GetCustomer : IQuery<CustomerDetailsDto>
    {
        public Guid CustomerId { get; set; }
    }
}
