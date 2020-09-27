using System;
using System.Threading;
using System.Threading.Tasks;
using FS.Api.Commands.Command;
using FS.Domain.Core.Interfaces;
using MediatR;

namespace FS.Api.Commands.Handlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            if (!IsValid(command)) throw new Exception("The new password must be different from the old one");

            var user = await _userRepository.Get(command.Id);
            user.SetPassword(command.NewPassword);

            await _userRepository.Update(command.Id, user);

            return user.Id;
        }

        private bool IsValid(ChangePasswordCommand command) => command.OldPassword != command.NewPassword;
    }
}