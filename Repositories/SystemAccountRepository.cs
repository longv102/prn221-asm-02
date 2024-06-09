using BO.Dtos;
using BO.Enums;
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

        public async Task<AccountOperationResult> AddAccount(SystemAccount account)
        {
            try
            {
                if (account is null)
                    return AccountOperationResult.EmptyAccount;
                
                var isExisted = _dbContext.SystemAccounts.Any(x => x.AccountId == account.AccountId);
                if (isExisted)
                    return AccountOperationResult.AccountExist;
                
                // Check email duplication
                var isDuplicatedEmail = _dbContext.SystemAccounts.Any(x => x.AccountEmail == account.AccountEmail);
                if (isDuplicatedEmail)
                    return AccountOperationResult.EmailExist;
                
                _dbContext.SystemAccounts.Add(account);
                await _dbContext.SaveChangesAsync();
                return AccountOperationResult.Success;
            }
            catch
            {
                throw;
            }
        }

        public async Task<SystemAccount?> Authenticate(AuthRequest request)
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

        public async Task<AccountOperationResult> DeleteAccount(short id)
        {
            try
            {
                var account = await _dbContext.SystemAccounts.FindAsync(id);
                if (account is null)
                    return AccountOperationResult.EmptyAccount;

                _dbContext.SystemAccounts.Remove(account);
                await _dbContext.SaveChangesAsync();
                return AccountOperationResult.Success;
            }
            catch
            {
                throw;
            }
        }

        public async Task<SystemAccount?> GetAccount(short id)
        {
            try
            {
                var account = await _dbContext.SystemAccounts.FindAsync(id);
                return account;
            }
            catch
            {
                throw;
            }
        }

        public Task<SystemAccount?> GetAccount(string email)
        {
            try
            {
                var account = _dbContext.SystemAccounts.FirstOrDefaultAsync(x => x.AccountEmail == email);
                return account;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SystemAccount>> GetAccounts()
        {
            try
            {
                var accounts = await _dbContext.SystemAccounts.ToListAsync();
                return accounts;
            }
            catch
            {
                throw;
            }
        }

        public async Task<AccountOperationResult> UpdateAccount(SystemAccount account)
        {
            try
            {
                if (account is null)
                    return AccountOperationResult.EmptyAccount;
                //
                _dbContext.SystemAccounts.Update(account);
                await _dbContext.SaveChangesAsync();
                return AccountOperationResult.Success;
            }
            catch
            {
                throw;
            }
        }
    }
}
