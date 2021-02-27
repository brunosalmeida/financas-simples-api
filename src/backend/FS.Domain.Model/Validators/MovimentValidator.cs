using FluentValidation;

namespace FS.Domain.Model.Validators
{
    public class MovementValidator : AbstractValidator<Movement>
    {
        public MovementValidator()
        {
            RuleFor(u => u.AccountId).NotEmpty().WithMessage("Movement must belongs a valid account.");
            
            RuleFor(u => u.Value).NotNull().WithMessage("Movement's value can not be null");
            RuleFor(u => u.Value).NotEmpty().WithMessage("Movement's value can not be empty");

            RuleFor(u => u.Description).NotNull().WithMessage("Movement's description can not be null");
            RuleFor(u => u.Description).NotEmpty().WithMessage("Movement's description can not be empty");
            RuleFor(u => u.Description).MaximumLength(100).WithMessage("Movement's description must have less than 100 characters");
            RuleFor(u => u.Description).MinimumLength(3).WithMessage("Movement's description must have more than 3 characters");
        }
    }
}