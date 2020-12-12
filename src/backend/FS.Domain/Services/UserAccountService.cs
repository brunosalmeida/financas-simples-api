namespace FS.Domain.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using Core.Interfaces;
    using DataObject.User;
    using Model;

    public class UserAccountService : IUserAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public UserAccountService(IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<UserAccount> Create(User user)
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