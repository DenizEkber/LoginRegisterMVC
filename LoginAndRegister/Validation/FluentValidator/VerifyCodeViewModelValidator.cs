using FluentValidation;
using LoginAndRegister.Models;

namespace LoginAndRegister.Validation.FluentValidator
{
    public class VerifyCodeViewModelValidator : AbstractValidator<VerifyCodeViewModel>
    {
        public VerifyCodeViewModelValidator() {
            RuleFor(x => x.EnteredCode)
                .NotEmpty().WithMessage("Code is required.");
        }
    }
}
