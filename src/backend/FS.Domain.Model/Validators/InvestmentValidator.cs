namespace FS.Domain.Model.Validators
{
    using FluentValidation;

    public class InvestmentValidator : AbstractValidator<Investment>
    {
        public InvestmentValidator()
        {
            RuleFor(u => u.AccountId).NotEmpty().WithMessage("Investment must belongs a valid account.");
            
            RuleFor(u => u.Value).NotNull().WithMessage("Investment's value can not be null");
            RuleFor(u => u.Value).NotEmpty().WithMessage("Investment's value can not be empty");

            RuleFor(u => u.Description).NotNull().WithMessage("Investment's description can not be null");
            RuleFor(u => u.Description).NotEmpty().WithMessage("Investment's description can not be empty");
            RuleFor(u => u.Description).MaximumLength(100).WithMessage("Investment's description must have less than 100 characters");
            RuleFor(u => u.Description).MinimumLength(3).WithMessage("Investment's description must have more than 3 characters");
        }
    }
}