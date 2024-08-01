using FluentValidation;
using LoginAndRegister.Models;

namespace LoginAndRegister.Validation.FluentValidator
{
    public class PasswordResetViewModelValidator : AbstractValidator<PasswordResetViewModel>
    {
        public PasswordResetViewModelValidator() { 
            RuleFor(x=>x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
        }
    }
}
