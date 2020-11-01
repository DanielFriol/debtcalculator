using FluentValidation;

namespace com.debtcalculator.Domain.Mediator.User.Update
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id).GreaterThan(0)
            .WithMessage("Id must be greater than 0");

            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Missing property name")
            .MaximumLength(80).WithMessage("Name can not have more than 80 characters");

            RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid Email")
            .NotEmpty().WithMessage("Missing property email")
            .MaximumLength(80).WithMessage("Email can not have more than 80 characters");
        }
    }
}