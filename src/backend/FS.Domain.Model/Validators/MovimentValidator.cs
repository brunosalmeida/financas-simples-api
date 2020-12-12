using FluentValidation;

namespace FS.Domain.Model.Validators
{
    public class MovimentValidator : AbstractValidator<Moviment>
    {
        public MovimentValidator()
        {
            RuleFor(u => u.AccountId).NotEmpty().WithMessage("Moviment must belongs a valid account.");
            
            RuleFor(u => u.Value).NotNull().WithMessage("Moviment's value can not be null");
            RuleFor(u => u.Value).NotEmpty().WithMessage("Moviment's value can not be empty");

            RuleFor(u => u.Description).NotNull().WithMessage("Moviment's description can not be null");
            RuleFor(u => u.Description).NotEmpty().WithMessage("Moviment's description can not be empty");
            RuleFor(u => u.Description).MaximumLength(100).WithMessage("Moviment's description must have less than 100 characters");
            RuleFor(u => u.Description).MinimumLength(3).WithMessage("Moviment's description must have more than 3 characters");
        }
    }
}