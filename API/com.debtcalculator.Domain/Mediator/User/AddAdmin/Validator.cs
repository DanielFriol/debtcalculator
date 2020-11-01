using FluentValidation;

namespace com.debtcalculator.Domain.Mediator.User.AddAdmin
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}