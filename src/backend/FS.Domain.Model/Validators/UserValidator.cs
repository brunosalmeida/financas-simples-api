using FluentValidation;

namespace FS.Domain.Model.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotNull().WithMessage("User's name can not be null");
            RuleFor(u => u.Name).NotEmpty().WithMessage("User's name can not be empty");
            RuleFor(u => u.Name).MaximumLength(100).WithMessage("User's name must have less than 100 characters");
            RuleFor(u => u.Name).MinimumLength(3).WithMessage("User's name must have more than 2 characters");

            RuleFor(u => u.Email).NotNull().WithMessage("User's email can not be null");
            RuleFor(u => u.Email).NotEmpty().WithMessage("User's email can not be empty");
            RuleFor(u => u.Email).EmailAddress().WithMessage("User's email must have a valid format");
            RuleFor(u => u.Email).MaximumLength(50).WithMessage("User's email must have less than 50 characters");
            RuleFor(u => u.Email).MinimumLength(10).WithMessage("User's email must have more than 10 characters");

            RuleFor(u => u.Password).NotNull().WithMessage("User's password can not be null");
            RuleFor(u => u.Password).NotEmpty().WithMessage("User's password can not be empty");
            RuleFor(u => u.Password).MaximumLength(20).WithMessage("User's password must have less than 20 characters");
            RuleFor(u => u.Password).MinimumLength(6).WithMessage("User's password must have more than 6 characters");
        }
    }
}