namespace FS.Api.Application.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using Domain.Core.Interfaces;
    using Domain.Core.Interfaces.Services;
    using Domain.Model;
    using Domain.Model.Validators;
    using MediatR;
    using Utils.Enums;

    public class CreateMovimentCommandHandler : IRequestHandler<CreateMovimentCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionService _transactionService;

        public CreateMovimentCommandHandler(IUserRepository repository, IAccountRepository accountRepository,
            ITransactionService transactionService)
        {
            _userRepository = repository;
            _accountRepository = accountRepository;
            _transactionService = transactionService;
        }

        public async Task<Guid> Handle(CreateMovimentCommand command,
            CancellationToken cancellationToken)
        {
            if (await _userRepository.Get(command.UserId) is var user && user is null)
                throw new Exception("Invalid user");

            if (await _accountRepository.Get(command.AccountId) is var account && account is null)
                throw new Exception("Invalid account");

            var moviment = new Moviment(command.Value, command.Description, (EMovimentCategory)command.Category,
                (EMovimentType)command.Type, command.AccountId, command.UserId);

            var movimentValidator = new MovimentValidator();
            var result = await movimentValidator.ValidateAsync(moviment, default);

            if (!result.IsValid) throw new Exception(String.Join("--", result.Errors));

            var balance = await _transactionService.CreateOrUpdateBalance(moviment);

            return balance.Id;
        }
    }
}