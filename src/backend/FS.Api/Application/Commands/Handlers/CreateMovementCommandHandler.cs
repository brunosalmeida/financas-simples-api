namespace FS.Api.Application.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using Domain.Core.Interfaces;
    using Domain.Model;
    using Domain.Model.Validators;
    using MediatR;
    using Utils.Enums;

    public class CreateMovementCommandHandler : IRequestHandler<CreateExpenseCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMovimentRepository _expenseRepository;

        public CreateMovementCommandHandler(IUserRepository repository, IAccountRepository accountRepository,
            IMovimentRepository expenseRepository)
        {
            _userRepository = repository;
            _accountRepository = accountRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<Guid> Handle(CreateExpenseCommand command,
            CancellationToken cancellationToken)
        {
            if (await _userRepository.Get(command.UserId) is var user && user is null)
                throw new Exception("Invalid user");

            if (await _accountRepository.Get(command.AccountId) is var account && account is null)
                throw new Exception("Invalid account");

            var movement = new Moviment(command.Value, command.Description, (EMovementCategory)command.Category,
                (EMovementType)command.Type, command.AccountId, command.UserId);

            var expenseValidator = new MovimentValidator();
            var result = await expenseValidator.ValidateAsync(movement, default);

            if (!result.IsValid) throw new Exception(String.Join("--", result.Errors));

            await _expenseRepository.Insert(movement);

            return movement.Id;
        }
    }
}