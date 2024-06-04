using BO.Dtos;

namespace Repositories.Contracts
{
    public interface ISystemAccountRepository
    {
        Task<SystemAccount> Authenticate(AuthRequest request);
    }
}
