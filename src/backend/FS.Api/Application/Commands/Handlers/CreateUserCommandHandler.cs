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
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<Guid?> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userId = await CreateUser(command);

            if (userId != null)
                CreateAccount(userId.Value);

            return userId;
        }

        private void CreateAccount(Guid userId)
        {
            var command = new CreateAccountCommand(userId);
            _mediator.Send(command);
        }

        private async Task<Guid?> CreateUser(CreateUserCommand command)
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