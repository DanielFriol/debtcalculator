using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.Enums;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.AddAdmin
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IMediator _mediator;

        public Handler(IUnitOfWork uow, IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMediator mediator)
        {
            _uow = uow;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _mediator = mediator;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetAsync(request.Id);

            user.TurnIntoAdmin();

            _userWriteRepository.Update(user);
            await _uow.CommitAsync();

            var emailSubject = "Você é um administrador";

            var emailBody = $"<p>Olá, {user.Name}!</p>" +
                                "<p>Sua conta no sistema Calculador de Dívida agora é um administrador!.<p/>";

            List<string> emails = new List<string>();
            emails.Add(user.Email);

            await _mediator.Publish(new Notification(emailSubject, emailBody, emails));

            return new Response(user);
        }
    }
}