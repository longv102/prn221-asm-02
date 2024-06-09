using BO.Dtos;
using BO.Enums;

namespace Repositories.Contracts
{
    public interface ISystemAccountRepository
    {
        Task<SystemAccount?> Authenticate(AuthRequest request);

        Task<IEnumerable<SystemAccount>> GetAccounts();

        Task<SystemAccount?> GetAccount(short id);

        Task<SystemAccount?> GetAccount(string email);

        Task<AccountOperationResult> AddAccount(SystemAccount account);

        Task<AccountOperationResult> UpdateAccount(SystemAccount account);

        Task<AccountOperationResult> DeleteAccount(short id);
    }
}
