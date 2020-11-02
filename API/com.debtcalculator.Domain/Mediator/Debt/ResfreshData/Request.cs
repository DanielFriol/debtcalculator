using MediatR;

namespace com.debtcalculator.Domain.Mediator.Debt.ResfreshData
{
    public class Request : IRequest<Response>
    {
        public long Id { get; set; }
    }
}