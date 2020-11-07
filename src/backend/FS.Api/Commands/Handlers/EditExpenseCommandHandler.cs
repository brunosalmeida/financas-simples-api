namespace FS.Api.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using Domain.Core.Interfaces;
    using Domain.Model.Validators;
    using MediatR;
    using Utils.Enums;

    public class EditExpenseCommandHandler : IRequestHandler<EditExpenseCommand, Guid>
    {
        private readonly IExpenseRepository _expenseRepository;

        public EditExpenseCommandHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<Guid> Handle(EditExpenseCommand command, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.Get(command.ExpenseId);
            expense.SetCategory((ECategory)command.Category); 
            expense.SetValue(command.Value); 
            expense.SetDescription(command.Description); 
            
            var expenseValidator = new ExpenseValidator();
            var result = await expenseValidator.ValidateAsync(expense, default);

            if (!result.IsValid) throw new Exception(String.Join("--", result.Errors));

            await _expenseRepository.Update(expense.Id, expense);

            return expense.Id;
        }
    }
}