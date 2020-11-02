using FluentValidation;

namespace com.debtcalculator.Domain.Mediator.Debt.UpdateConfig
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.InterestType)
            .NotEmpty().WithMessage("Missing property Interest Type")
            .GreaterThan(0).WithMessage("Interest Type must be greater than 0")
            .LessThan(3).WithMessage("Interest Type must be less than 3");

            RuleFor(x => x.MaxSplit)
            .NotEmpty().WithMessage("Missing property Max Split");

            RuleFor(x => x.Interest)
            .NotEmpty().WithMessage("Missing property Max Split");

            RuleFor(x => x.PaschoalottoPercentage)
            .NotEmpty().WithMessage("Missing property Paschoalotto Percentage");
        }
    }
}