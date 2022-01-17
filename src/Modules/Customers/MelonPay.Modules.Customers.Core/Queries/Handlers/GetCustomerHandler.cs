﻿using Microsoft.EntityFrameworkCore;
using MelonPay.Modules.Customers.Core.DAL;
using MelonPay.Modules.Customers.Core.DTO;
using MelonPay.Shared.Abstractions.Queries;

namespace MelonPay.Modules.Customers.Core.Queries.Handlers
{
    internal class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDetailsDto>
    {
        private readonly CustomersDbContext _dbContext;

        public GetCustomerHandler(CustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerDetailsDto> HandleAsync(GetCustomer query, CancellationToken cancellationToken = default)
        {
            var customer = await _dbContext.Customers
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == query.CustomerId, cancellationToken);

            return customer?.AsDetailsDto();
        }
    }
}
