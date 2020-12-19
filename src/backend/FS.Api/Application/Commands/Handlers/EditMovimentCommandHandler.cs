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

    public class EditMovimentCommandHandler : IRequestHandler<EditMovimentCommand, Guid>
    {
        private readonly IMovimentRepository _expenseRepository;

        public EditMovimentCommandHandler(IMovimentRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<Guid> Handle(EditMovimentCommand command, CancellationToken cancellationToken)
        {
            var movement = await _expenseRepository.Get(command.MovementId);
            movement.SetCategory((EMovimentCategory)command.Category); 
            movement.SetValue(command.Value); 
            movement.SetDescription(command.Description); 
            movement.SetMovimentType((EMovimentType)command.Type);
            
            var movimentValidatorValidator = new MovimentValidator();
            var result = await movimentValidatorValidator.ValidateAsync(movement, default);

            if (!result.IsValid) throw new Exception(String.Join("--", result.Errors));

            await _expenseRepository.Update(movement.Id, movement);

            return movement.Id;
        }
    }
}