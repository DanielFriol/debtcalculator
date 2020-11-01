using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.Delete
{
    public class Request : IRequest<Response>
    {
        public long Id { get; set; }
    }
}