using FluentValidation;

namespace FS.Domain.Model.Validators
{
    public class ExpenseValidator : AbstractValidator<Expense>
    {
        public ExpenseValidator()
        {
            RuleFor(u => u.AccountId).NotEmpty().WithMessage("Expense must belongs a valid account.");
            
            RuleFor(u => u.Value).NotNull().WithMessage("Expense's value can not be null");
            RuleFor(u => u.Value).NotEmpty().WithMessage("Expense's value can not be empty");

            RuleFor(u => u.Description).NotNull().WithMessage("Expense's description can not be null");
            RuleFor(u => u.Description).NotEmpty().WithMessage("Expense's description can not be empty");
            RuleFor(u => u.Description).MaximumLength(100).WithMessage("Expense's description must have less than 100 characters");
            RuleFor(u => u.Description).MinimumLength(3).WithMessage("Expense's description must have more than 3 characters");
        }
    }
}