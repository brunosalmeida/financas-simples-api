namespace FS.Api.Application.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Command;
    using Domain.Core.Interfaces;
    using Domain.Model.Validators;
    using MediatR;
    using Utils.Enums;

    public class EditExpenseCommandHandler : IRequestHandler<EditMovementCommand, Guid>
    {
        private readonly IMovimentRepository _expenseRepository;

        public EditExpenseCommandHandler(IMovimentRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<Guid> Handle(EditMovementCommand command, CancellationToken cancellationToken)
        {
            var movement = await _expenseRepository.Get(command.MovementId);
            movement.SetCategory((EMovementCategory)command.Category); 
            movement.SetValue(command.Value); 
            movement.SetDescription(command.Description); 
            movement.SetExpenseType((EMovementType)command.Type);
            
            var expenseValidator = new ExpenseValidator();
            var result = await expenseValidator.ValidateAsync(movement, default);

            if (!result.IsValid) throw new Exception(String.Join("--", result.Errors));

            await _expenseRepository.Update(movement.Id, movement);

            return movement.Id;
        }
    }
}