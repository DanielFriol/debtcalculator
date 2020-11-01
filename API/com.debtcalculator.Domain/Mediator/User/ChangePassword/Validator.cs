using FluentValidation;

namespace com.debtcalculator.Domain.Mediator.User.ChangePassword
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Missing property New Password");

            RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid Email")
            .NotEmpty().WithMessage("Missing property email")
            .MaximumLength(80).WithMessage("Email can not have more than 80 characters");
            
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Missing property password");
        }
    }
}