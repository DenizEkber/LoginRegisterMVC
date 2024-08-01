using FluentValidation;
using LoginAndRegister.Models;

namespace LoginAndRegister.Validation.FluentValidator
{
    public class SetPasswordViewModelValidator :AbstractValidator<SetPasswordViewModel>
    {
        public SetPasswordViewModelValidator() { 
            RuleFor(x=>x.CreatePassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x=>x.ReEnterPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.CreatePassword).WithMessage("Passwords do not match.");
        }
    }
}
