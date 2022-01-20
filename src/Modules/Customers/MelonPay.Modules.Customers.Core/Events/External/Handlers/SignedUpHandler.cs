﻿using Microsoft.Extensions.Logging;
using MelonPay.Shared.Abstractions.Time;
using MelonPay.Shared.Abstractions.Events;
using MelonPay.Modules.Customers.Core.Domain.Entities;
using MelonPay.Modules.Customers.Core.Domain.Repositories;

namespace MelonPay.Modules.Customers.Core.Events.External.Handlers
{
    internal class SignedUpHandler : IEventHandler<SignedUp>
    {
        private const string ValidRole = "user";
        private readonly ICustomerRepository _customerRepository;
        private readonly IClock _clock;
        private readonly ILogger<SignedUpHandler> _logger;

        public SignedUpHandler(ICustomerRepository customerRepository, IClock clock, ILogger<SignedUpHandler> logger)
        {
            _customerRepository = customerRepository;
            _clock = clock;
            _logger = logger;
        }

        public async Task HandleAsync(SignedUp @event, CancellationToken cancellationToken = default)
        {
            if (@event.Role is not ValidRole)
            {
                return;
            }

            var customer = new Customer(@event.UserId, @event.Email, _clock.CurrentDate());
            await _customerRepository.AddAsync(customer);
            _logger.LogInformation($"Created a new customer based on user with ID: '{@event.UserId}'.");
        }
    }
}
