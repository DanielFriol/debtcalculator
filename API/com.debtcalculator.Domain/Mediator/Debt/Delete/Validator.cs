using FluentValidation;

namespace com.debtcalculator.Domain.Mediator.Debt.Delete
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x=> x.Id).NotEmpty().WithMessage("Missing property id")
            .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}