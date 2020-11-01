using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.AddAdmin
{
    public class Request : IRequest<Response>
    {
       public long Id { get; set; }
    }
}