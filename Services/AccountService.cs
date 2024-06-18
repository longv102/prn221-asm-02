using AutoMapper;
using BO.Dtos;
using BO.Enums;
using Repositories;
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

        public async Task<SystemAccountDto?> Authenticate(AuthRequest request)
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

        public async Task<AccountOperationResult> CreateAccount(SystemAccountDto request)
        {
            if (request is null)
                throw new NullReferenceException("Request is nullable!");

            var account = _mapper.Map<SystemAccount>(request);
            // Hard-code for password validation
            account.AccountPassword = "1234";

            var result = await _systemAccountRepository.AddAccount(account);
            return result;
        }

        public async Task<AccountOperationResult> DeleteAccount(short accountId)
        {
            var result = await _systemAccountRepository.DeleteAccount(accountId);
            return result;
        }

        public async Task<SystemAccountDto> GetAccount(short accountId)
        {
            var account = await _systemAccountRepository.GetAccount(accountId);
            var mappedAccount = _mapper.Map<SystemAccountDto>(account);
            return mappedAccount;
        }

        public async Task<SystemAccountDto> GetAccount(string email)
        {
            var account = await _systemAccountRepository.GetAccount(email);
            var response = _mapper.Map<SystemAccountDto>(account);
            return response;
        }

        public async Task<IEnumerable<SystemAccountDto>> GetAccounts()
        {
            var accounts = await _systemAccountRepository.GetAccounts();
            var response = _mapper.Map<IEnumerable<SystemAccountDto>>(accounts);
            return response;
        }

        public async Task<AccountOperationResult> UpdateAccount(SystemAccountDto request)
        {
            if (request is null)
                throw new NullReferenceException("Request is nullable!");

            var existedAccount = await _systemAccountRepository.GetAccount(request.AccountId);
            // Update account
            var updatedAccount = _mapper.Map<SystemAccountDto, SystemAccount>(request, existedAccount);
            var result = await _systemAccountRepository.UpdateAccount(updatedAccount);
            return result;
        }
    }
}
