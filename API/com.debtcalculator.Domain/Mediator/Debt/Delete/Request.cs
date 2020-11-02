using MediatR;

namespace com.debtcalculator.Domain.Mediator.Debt.Delete
{
    public class Request : IRequest<Response>
    {
        public long Id { get; set; }
    }
}