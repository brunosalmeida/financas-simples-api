namespace FS.Api.Application.Commands.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using DataObject.User;
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
           
            var password = PasswordHelper.Encrypt(command.Password);

            var user = new User(command.Name, command.Email, password, command.Gender);

            var userAccount = await _userAccountService.Create(user);
            
            return userAccount;
        }
    }
}