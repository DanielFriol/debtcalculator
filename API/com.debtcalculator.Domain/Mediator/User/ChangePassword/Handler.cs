using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.Helpers;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.ChangePassword
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
            var password = request.OldPassword.Encrypt(user.Salt);
            switch (request.GrantType)
            {
                case "new_password":
                    if (!user.Password.Equals(password))
                        return new Response().AddError("The current password does not match");

                    user.UpdatePassword(request.NewPassword);
                    await _uow.CommitAsync();
                    SendEmail(user);
                    break;
                case "forgot_password":
                    if (!user.ChangePasswordCode.Equals(request.VerificationCode.Encrypt(user.Salt)) && user.CodeExpiration < DateTime.UtcNow)
                        return new Response().AddError("Invalid verification code");

                    user.UpdatePassword(request.NewPassword);
                    await _uow.CommitAsync();
                    SendEmail(user);
                    break;
            }

            return new Response();
        }

        public async void SendEmail(Domain.Entities.User user)
        {
            var emailSubject = "Sua senha foi alterada";
            var emailBody = $"<p>Olá, {user.Name}!</p>" +
                                 "<p>Sua senha foi alterada com sucesso.<p/>" +
                                 "<br>" +
                                 "<p>Caso você não tenha realizado essa alteração, entre imediatamente em contato com o <a mailto=\"danielfriol@gmail.com\">danielfriol@gmail.com</a></P>";

            List<string> emails = new List<string>();
            emails.Add(user.Email);
            await _mediator.Publish(new Notification(emailSubject, emailBody, emails));
        }
    }
}