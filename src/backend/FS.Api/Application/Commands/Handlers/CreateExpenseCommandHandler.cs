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

    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IExpenseRepository _expenseRepository;

        public CreateExpenseCommandHandler(IUserRepository repository, IAccountRepository accountRepository,
            IExpenseRepository expenseRepository)
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

            var expense = new Expense(command.Value, command.Description, (ECategory)command.Category,
                command.AccountId);

            var expenseValidator = new ExpenseValidator();
            var result = await expenseValidator.ValidateAsync(expense, default);

            if (!result.IsValid) throw new Exception(String.Join("--", result.Errors));

            await _expenseRepository.Insert(expense);

            return expense.Id;
        }
    }
}