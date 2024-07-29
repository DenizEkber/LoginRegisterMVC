using FluentValidation;
using LoginAndRegister.Models;

namespace LoginAndRegister.Validation.FluentValidator
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator() {
            RuleFor(X => X.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(X => X.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
