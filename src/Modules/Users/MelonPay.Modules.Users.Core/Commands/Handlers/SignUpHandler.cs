using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using MelonPay.Shared.Abstractions.Time;
using MelonPay.Shared.Abstractions.Commands;
using MelonPay.Shared.Abstractions.Messaging;
using MelonPay.Modules.Users.Core.Events;
using MelonPay.Modules.Users.Core.Exceptions;
using MelonPay.Modules.Users.Core.Domain.Entities;
using MelonPay.Modules.Users.Core.Domain.Repositories;

namespace MelonPay.Modules.Users.Core.Commands.Handlers
{
    internal sealed class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RegistrationOptions _registrationOptions;
        private readonly IMessageBroker _messageBroker;
        private readonly IClock _clock;
        private readonly ILogger<SignUpHandler> _logger;

        public SignUpHandler(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPasswordHasher<User> passwordHasher,
            RegistrationOptions registrationOptions,
            IMessageBroker messageBroker,
            IClock clock,
            ILogger<SignUpHandler> logger
            )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _registrationOptions = registrationOptions;
            _messageBroker = messageBroker;
            _clock = clock;
            _logger = logger;
        }

        public async Task HandleAsync(SignUp command, CancellationToken cancellationToken = default)
        {
            if (_registrationOptions.Enabled)
            {
                throw new SignUpDisabledException();
            }

            var email = command.Email.ToLowerInvariant();
            var provider = email.Split("@").Last();
            if (_registrationOptions.InvalidEmailProviders?.Any(x => provider.Contains(x)) is true)
            {
                throw new InvalidEmailException(email);
            }

            if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length is < 6 or > 100)
            {
                throw new InvalidPasswordException("not matching the criteria.");
            }

            var user = await _userRepository.GetAsync(email);
            if (user is not null)
            {
                throw new EmailInUseException();
            }

            var roleName = string.IsNullOrWhiteSpace(command.Role) ? Role.Default : command.Role.ToLowerInvariant();
            var role = await _roleRepository.GetAsync(roleName);
            if (role is null)
            {
                throw new RoleNotFoundException(roleName);
            }

            var now = _clock.CurrentDate();
            var password = _passwordHasher.HashPassword(default, command.Password);
            user = new User
            {
                Id = command.UserId,
                Email = command.Email,
                Password = password,
                Role = role,
                CreatedAt = now,
                State = UserState.Active
            };

            await _userRepository.UpdateAsync(user);
            await _messageBroker.PublishAsync(new SignedUp(user.Id, user.Email, user.Role.Name), cancellationToken);
            _logger.LogInformation($"User with ID: '{user.Id}' has signed up.");
        }
    }
}
