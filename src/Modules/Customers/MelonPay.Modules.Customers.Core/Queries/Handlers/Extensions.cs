﻿using MelonPay.Modules.Customers.Core.Domain.Entities;
using MelonPay.Modules.Customers.Core.DTO;

namespace MelonPay.Modules.Customers.Core.Queries.Handlers
{
    internal static class Extensions
    {
        public static CustomerDto? AsDto(this Customer customer)
            => Map<CustomerDto>(customer);

        public static CustomerDetailsDto? AsDetailsDto(this Customer customer)
        {
            var dto = Map<CustomerDetailsDto>(customer);
            dto.Address = customer.Address;
            dto.Notes = customer.Notes;
            dto.Identity = customer.Identity is null
                ? null
                : new()
                {
                    Type = customer.Identity.Type,
                    Series = customer.Identity.Series
                };

            return dto;
        }

        private static T Map<T>(this Customer customer) where T : CustomerDto, new()
            => new()
            {
                Id = customer.Id,
                Email = customer.Email,
                Name = customer.Name,
                FullName = customer.FullName,
                Nationality = customer.Nationality,
                IsActive = customer.IsActive,
                CreatedAt = customer.CreatedAt
            };
    }
}
