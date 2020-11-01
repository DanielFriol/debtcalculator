using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.Enums;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.Add
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
            if (await this._userReadRepository.GetByEmailAsync(request.Email) != null)
                return new Response().AddError("Email already registered");

            var user = new Domain.Entities.User(request.Name, request.Email, (int)UserProfile.User, request.Password);

            _userWriteRepository.Add(user);
            await _uow.CommitAsync();

            var emailSubject = "Seja bem vindo ao Calculador de Dívida!";

            var emailBody = $"<p>Olá, {user.Name}!</p>" +
                                "<p>Sua conta de usuário no sistema Calculador de Dívida foi criada com sucesso!.<p/>";

            List<string> emails = new List<string>();
            emails.Add(user.Email);

            await _mediator.Publish(new Notification(emailSubject, emailBody, emails));

            return new Response(user);
        }
    }
}