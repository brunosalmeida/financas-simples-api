namespace FS.Api.Application.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using Domain.Core.Interfaces;
    using MediatR;

    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public EditUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<Guid> Handle(EditUserCommand command, CancellationToken cancellationToken)
        {
            if (!IsValid(command)) throw new Exception("Name and email can not be empty");

            var user = await _userRepository.Get(command.Id);
            user.SetEmail(command.Email);
            user.SetName(command.Name);

            await _userRepository.Update(user.Id, user);

            return user.Id;
        }

        private bool IsValid(EditUserCommand command) =>
            !string.IsNullOrEmpty(command.Name) && !string.IsNullOrEmpty(command.Email);
    }
}