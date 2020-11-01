using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.ForgotPassword
{
    public class Request : IRequest<Response>
    {
        public string Email { get; set; }
    }
}