using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.ChangePassword
{
    public class Request : IRequest<Response>
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string GrantType { get; set; } = "new_password";

    }
}