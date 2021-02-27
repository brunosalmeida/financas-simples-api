using FluentValidation;

namespace FS.Domain.Model.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(u => u.User).SetValidator(new UserValidator());
            RuleForEach(u => u.Expenses).SetValidator(new MovementValidator());
        }
    }
}