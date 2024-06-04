using BO.Dtos;
using Repositories;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        Task<SystemAccountDto> Authenticate(AuthRequest request);
    }
}
