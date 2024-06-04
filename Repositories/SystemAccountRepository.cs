using BO.Dtos;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Databases;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly FunewsManagementDbContext _dbContext;

        public SystemAccountRepository(FunewsManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SystemAccount> Authenticate(AuthRequest request)
        {
            try
            {
                var user = await _dbContext.SystemAccounts
                    .FirstOrDefaultAsync(x => x.AccountEmail == request.Email && x.AccountPassword == request.Password);
                return user;
            }
            catch
            {
                throw;
            }
        }
    }
}
