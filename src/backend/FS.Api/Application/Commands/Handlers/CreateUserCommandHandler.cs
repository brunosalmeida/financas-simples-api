namespace FS.Api.Application.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using Domain.Core.Interfaces;
    using Domain.Model;
    using MediatR;
    using Utils.Helpers;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid?>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid?> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (!IsValid(command)) return null;

            var password = PasswordHelper.Encrypt(command.Password);
            
            var user = new User(command.Name, command.Email, password);

            await _userRepository.Insert(user);

            return user.Id;
        }

        private bool IsValid(CreateUserCommand command) =>
            !string.IsNullOrEmpty(command.Name) && !string.IsNullOrEmpty(command.Email) &&
            !string.IsNullOrEmpty(command.Password);
    }
}