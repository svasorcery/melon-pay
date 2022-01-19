using Microsoft.EntityFrameworkCore;
using MelonPay.Shared.Abstractions.Queries;
using MelonPay.Modules.Users.Core.DAL;
using MelonPay.Modules.Users.Core.DTO;

namespace MelonPay.Modules.Users.Core.Queries.Handlers
{
    internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDetailsDto>
    {
        private readonly UsersDbContext _dbContext;

        public GetUserHandler(UsersDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<UserDetailsDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Id == query.UserId, cancellationToken);

            return user?.AsDetailsDto();
        }
    }
}
