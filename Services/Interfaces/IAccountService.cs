using BO.Dtos;
using BO.Enums;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        Task<SystemAccountDto?> Authenticate(AuthRequest request);

        Task<IEnumerable<SystemAccountDto>> GetAccounts();

        Task<SystemAccountDto> GetAccount(short accountId);

        Task<SystemAccountDto> GetAccount(string email);

        Task<AccountOperationResult> CreateAccount(SystemAccountDto request);

        Task<AccountOperationResult> UpdateAccount(SystemAccountDto request);

        Task<AccountOperationResult> DeleteAccount(short accountId);
    }
}
