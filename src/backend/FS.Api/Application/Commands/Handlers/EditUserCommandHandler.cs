namespace FS.Api.Application.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using Domain.Core.Interfaces;
    using Domain.Model.Validators;
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
            var user = await _userRepository.Get(command.Id);
            user.SetEmail(command.Email);
            user.SetName(command.Name);
            
            var userValidator = new UserValidator();
            var result = await userValidator.ValidateAsync(user, default);

            if (!result.IsValid) throw new Exception(String.Join("--", result.Errors));

            await _userRepository.Update(user.Id, user);

            return user.Id;
        }
    }
}