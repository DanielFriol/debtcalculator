using MediatR;

namespace com.debtcalculator.Domain.Mediator.SignIn
{
    public class Request : IRequest<Response>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}