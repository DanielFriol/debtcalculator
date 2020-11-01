using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.Update
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
            var UserEmailVerify = await _userReadRepository.GetByEmailAsync(request.Email);
            if (user.Id != UserEmailVerify.Id)
                return new Response().AddError("Email already registered");

            user.Update(request.Name, request.Email);
            _userWriteRepository.Update(user);
            await _uow.CommitAsync();

            var emailSubject = "Alteração de conta";

            var emailBody = $"<p>Olá, {user.Name}!</p>" +
                                "<p>Sua conta de usuário no sistema Calculador de Dívida alterada com sucesso!<p/>";

            List<string> emails = new List<string>();
            emails.Add(user.Email);

            await _mediator.Publish(new Notification(emailSubject, emailBody, emails));

            return new Response(user);
        }
    }
}