using FluentValidation;

namespace com.debtcalculator.Domain.Mediator.SignIn
{
    public class Validator : AbstractValidator<Request>
    {

        public Validator()
        {

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid Email")
                .NotEmpty().WithMessage("Missing property email");

            RuleFor(m => m.Password)
                .NotEmpty().WithMessage("Missing property password");

        }
    }
}