using AutoMapper;
using BO.Dtos;
using Repositories.Contracts;
using Services.Interfaces;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly ISystemAccountRepository _systemAccountRepository;
        private readonly IMapper _mapper;

        public AccountService(ISystemAccountRepository systemAccountRepository, IMapper mapper)
        {
            _systemAccountRepository = systemAccountRepository;
            _mapper = mapper;
        }

        public async Task<SystemAccountDto> Authenticate(AuthRequest request)
        {
            try
            {
                var user = await _systemAccountRepository.Authenticate(request);
                var mappedUser = _mapper.Map<SystemAccountDto>(user);
                return mappedUser;
            }
            catch
            {
                throw;
            }
        }
    }
}
