namespace FS.Api.Application.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using DataObject.User;
    using Domain.Core.Interfaces;
    using Domain.Core.Interfaces.Services;
    using Domain.Model;
    using Domain.Model.Validators;
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
            var password = PasswordHelper.Encrypt(command.Password);

            var user = new User(command.Name, command.Email, password, command.Gender, command.BirthDate);
            
            var userValidator = new UserValidator();
            var result = await userValidator.ValidateAsync(user, default);

            if (!result.IsValid) throw new Exception(String.Join("--", result.Errors));
            
            var userAccount = await _userAccountService.CreateUserAndAccount(user);
            
            return userAccount;
        }
    }
}