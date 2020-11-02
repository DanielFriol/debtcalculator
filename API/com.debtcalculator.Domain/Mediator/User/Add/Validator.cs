using FluentValidation;

namespace com.debtcalculator.Domain.Mediator.User.Add
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Missing property name")
            .MaximumLength(80).WithMessage("Name can not have more than 80 characters");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Missing property password");


            RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid Email")
            .NotEmpty().WithMessage("Missing property email")
            .MaximumLength(80).WithMessage("Email can not have more than 80 characters");

            RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("Missing property CPF")
            .MinimumLength(11).WithMessage("CPF must have 11 characters").MaximumLength(11).WithMessage("CPF must have 11 characters");
        }
    }
}