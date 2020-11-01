using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.Add
{
    public class Request : IRequest<Response>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}