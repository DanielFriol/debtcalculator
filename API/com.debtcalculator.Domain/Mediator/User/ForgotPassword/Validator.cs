using FluentValidation;

namespace com.debtcalculator.Domain.Mediator.User.ForgotPassword
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
             .EmailAddress().WithMessage("Invalid Email")
             .NotEmpty().WithMessage("Missing property email")
             .MaximumLength(80).WithMessage("Email can not have more than 80 characters");
        }
    }
}