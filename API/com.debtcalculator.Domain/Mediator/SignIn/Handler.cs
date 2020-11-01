using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.Helpers;
using com.debtcalculator.Domain.Models;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.SignIn
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IUserReadRepository _userReadRepository;

        public Handler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var userData = await _userReadRepository.GetByEmailAsync(request.Email);
            var validCredentials = (userData != null && request.Password.Encrypt(userData.Salt) == userData.Password);

            if (!validCredentials) return new Response().AddError("Invalid Credentials");

            return new Response(new UserModel(userData.Id, userData.Name, userData.Email, userData.IdProfile));

        }
    }
}