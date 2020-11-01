using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.Update
{
    public class Request : IRequest<Response>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}