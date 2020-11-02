using FluentValidation;

namespace com.debtcalculator.Domain.Mediator.Debt.Add
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.CLientCPF)
            .NotEmpty().WithMessage("Missing property CPF")
            .MinimumLength(11).WithMessage("CPF must have 11 characters").MaximumLength(11).WithMessage("CPF must have 11 characters");

            RuleFor(x => x.DueDate).NotEmpty().WithMessage("Missing property Due Date");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Missing property Value");
            RuleFor(x=> x.ContactPhone).NotEmpty().WithMessage("Missing property Contct Phone")
            .MaximumLength(30).WithMessage("Contact phone max length is 30");
        }
    }
}