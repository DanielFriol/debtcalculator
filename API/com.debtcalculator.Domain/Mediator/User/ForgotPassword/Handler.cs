using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.ForgotPassword
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
            var user = await _userReadRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return new Response().AddError("Email is not registered");

            var newCode = _userWriteRepository.GenerateVerificationCode();

            user.UpdateChangePasswordCode(newCode);
            await _uow.CommitAsync();

            var emailSubject = "Código de Verificação";

            var emailBody = "<tr>" +
                                "<tr>" +
                                    "<td align = \"center\" valign = \"middle\" width = \"100%\" style = \"font-family: Arial, sans-serif, 'Lato'; color: #585858; font-size: 22px; line-height: 1; padding-top: 48px;\" >" +
                                        $"Olá, {user.Name}" +
                                    "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td align = \"center\" valign = \"middle\" width = \"100%\" style = \"font-family: Arial, sans-serif, 'Lato'; font-size: 32px; line-height: 1; padding-top: 35px;\" >" +
                                        "Aqui está seu código de verificação para a troca de senha: " +
                                    "</td>" +
                                "</tr>" +
                              "</tr>" +
                              "<tr>" +
                                    "<td align=\"center\" width=\"100%\" style =\"font-family: Arial, sans-serif, 'Lato'; color: #000000; font-size: 42px; line-height: 1; padding-top: 0px;\">" +
                                         $"{newCode}" +
                                     "</td>" +
                                  "</tr>";

            List<string> emails = new List<string>();
            emails.Add(user.Email);

            await _mediator.Publish(new Notification(emailSubject, emailBody, emails));

            return new Response();
        }
    }
}