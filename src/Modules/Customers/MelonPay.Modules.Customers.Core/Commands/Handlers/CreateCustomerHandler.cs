using Microsoft.Extensions.Logging;
using MelonPay.Shared.Abstractions.Time;
using MelonPay.Shared.Kernel.ValueObjects;
using MelonPay.Shared.Abstractions.Commands;
using MelonPay.Modules.Customers.Core.Clients;
using MelonPay.Modules.Customers.Core.Exceptions;
using MelonPay.Modules.Customers.Core.Domain.Entities;
using MelonPay.Modules.Customers.Core.Domain.Repositories;

namespace MelonPay.Modules.Customers.Core.Commands.Handlers
{
    internal sealed class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserApiClient _userApiClient;
        private readonly IClock _clock;
        private readonly ILogger<CreateCustomerHandler> _logger;

        public CreateCustomerHandler(ICustomerRepository customerRepository, IUserApiClient userApiClient, IClock clock, ILogger<CreateCustomerHandler> logger)
        {
            _customerRepository = customerRepository;
            _userApiClient = userApiClient;
            _clock = clock;
            _logger = logger;
        }

        public async Task HandleAsync(CreateCustomer command, CancellationToken cancellationToken = default)
        {
            _ = new Email(command.Email);

            var user = await _userApiClient.GetAsync(command.Email);
            if (user is null)
            {
                return;
            }

            if (user.Role is not "user")
            {
                return;
            }

            var customerId = user.UserId;
            if (await _customerRepository.GetAsync(customerId) is not null)
            {
                throw new CustomerAlreadyExistsException(customerId);
            }

            var customer = new Customer(customerId, command.Email, _clock.CurrentDate());
            await _customerRepository.AddAsync(customer);
            _logger.LogInformation($"Created a customer with ID: '{customer.Id}'");
        }
    }
}
