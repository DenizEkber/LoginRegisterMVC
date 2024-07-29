using FluentValidation;
using LoginAndRegister.Models;

namespace LoginAndRegister.Validation.FluentValidator
{
    public class RegisterViewModelValidator :AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator() {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+\d{1,3}\s\d{1,3}\s\d{3}\s\d{3}\s\d{2}$").WithMessage("A valid phone number is required.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");
            RuleFor(x => x.Agree)
                .Equal(false).WithMessage("You must agree to the terms and privacy policies.");
        }
    }
}
