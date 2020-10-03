using System;
using System.Threading;
using System.Threading.Tasks;
using FS.Api.Commands.Command;
using FS.Domain.Core.Interfaces;
using MediatR;

namespace FS.Api.Commands.Handlers
{
    using Utils.Helpers;

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            if (PasswordHelper.Compare(command.OldPassword, command.NewPassword))
                throw new Exception("The new password must be different from the old one");

            var newPassword = PasswordHelper.Encrypt(command.NewPassword);

            var user = await _userRepository.Get(command.Id);
            user.SetPassword(newPassword);

            await _userRepository.Update(command.Id, user);

            return user.Id;
        }
    }
}