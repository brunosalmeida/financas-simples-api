namespace FS.Domain.Core.Services
{
    using System;
    using System.Threading.Tasks;
    using DataObject.User;
    using Interfaces;
    using Model;
    using Utils.Helpers;

    public class UserAccountService : IUserAccountService
    {
        public IAccountRepository _accountRepository { get; private set; }
        public IUserRepository _userRepository { get; private set; }

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