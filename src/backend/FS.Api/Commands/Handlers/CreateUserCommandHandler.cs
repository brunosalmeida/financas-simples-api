using System;
using System.Threading;
using System.Threading.Tasks;
using FS.Api.Commands.Command;
using FS.Domain.Core.Interfaces;
using FS.Domain.Model;
using MediatR;

namespace FS.Api.Commands.Handlers
{
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