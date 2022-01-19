using Microsoft.EntityFrameworkCore;
using MelonPay.Shared.Abstractions.Queries;
using MelonPay.Modules.Users.Core.DAL;
using MelonPay.Modules.Users.Core.DTO;

namespace MelonPay.Modules.Users.Core.Queries.Handlers
{
    internal sealed class GetUserByEmailHandler : IQueryHandler<GetUserByEmail, UserDetailsDto>
    {
        private readonly UsersDbContext _dbContext;

        public GetUserByEmailHandler(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDetailsDto> HandleAsync(GetUserByEmail query, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Email == query.Email, cancellationToken);

            return user?.AsDetailsDto();
        }
    }
}
