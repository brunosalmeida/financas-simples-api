namespace FS.Domain.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using Core.Interfaces;
    using DataObject.User;
    using Interfaces.Services;
    using Model;

    public class CreateUserService : IUserAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public CreateUserService(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<UserAccount> CreateUserAndAccount(User user)
        {
            try
            {
                await _userRepository.Insert(user);

                var account = new Account(user);
                await _accountRepository.Insert(account);

                return new UserAccount(user.Id, account.Id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}