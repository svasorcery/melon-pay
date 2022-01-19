using Microsoft.EntityFrameworkCore;
using MelonPay.Shared.Abstractions.Queries;
using MelonPay.Shared.Abstractions.Queries.Pagination;
using MelonPay.Shared.Infrastructure.Postgres;
using MelonPay.Modules.Users.Core.DAL;
using MelonPay.Modules.Users.Core.DTO;
using MelonPay.Modules.Users.Core.Domain.Entities;

namespace MelonPay.Modules.Users.Core.Queries.Handlers
{
    internal sealed class BrowseUsersHandler : IQueryHandler<BrowseUsers, Paged<UserDto>>
    {
        private readonly UsersDbContext _dbContext;

        public BrowseUsersHandler(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Paged<UserDto>> HandleAsync(BrowseUsers query, CancellationToken cancellationToken = default)
        {
            var users = _dbContext.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                users = users.Where(x => x.Email == query.Email);
            }

            if (!string.IsNullOrWhiteSpace(query.Role))
            {
                users = users.Where(x => x.RoleId == query.Role);
            }

            if (!string.IsNullOrWhiteSpace(query.State) && Enum.TryParse<UserState>(query.State, true, out var state))
            {
                users = users.Where(x => x.State == state);
            }

            return users.AsNoTracking()
                .Include(x => x.Role)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.AsDto())
                .PaginateAsync(query, cancellationToken);
        }
    }
}
