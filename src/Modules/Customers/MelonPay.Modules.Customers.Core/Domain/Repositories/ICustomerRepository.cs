﻿using MelonPay.Modules.Customers.Core.Domain.Entities;

namespace MelonPay.Modules.Customers.Core.Domain.Repositories
{
    internal interface ICustomerRepository
    {
        Task<bool> ExistsAsync(string name);
        Task<Customer> GetAsync(Guid id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }
}
