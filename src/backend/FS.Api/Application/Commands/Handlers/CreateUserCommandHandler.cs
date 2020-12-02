namespace FS.Api.Application.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using DataObject.User;
    using Domain.Core.Interfaces;
    using Domain.Core.Services;
    using Domain.Model;
    using MediatR;
    using Utils.Helpers;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserAccount>
    {
        private readonly IUserAccountService _userAccountService;

        public CreateUserCommandHandler(IUserAccountService accountService)
        {
            _userAccountService = accountService;
        }

        public async Task<UserAccount> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (!IsValid(command)) return null;

            var password = PasswordHelper.Encrypt(command.Password);

            var user = new User(command.Name, command.Email, password);

            var userAccount = await _userAccountService.Create(user);
            
            return userAccount;
        }


        private bool IsValid(CreateUserCommand command) =>
            !string.IsNullOrEmpty(command.Name) && !string.IsNullOrEmpty(command.Email) &&
            !string.IsNullOrEmpty(command.Password);
    }
}