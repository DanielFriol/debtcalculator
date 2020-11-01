using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra.Services;
using com.debtcalculator.Domain.DTOs.Infra.Serivces;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.ChangePassword
{
    public class SendEmail : INotificationHandler<Notification>
    {
        private readonly ISendEmailService _sendEmailService;

        public SendEmail(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            var emailData = new EmailMessage()
            {
                To = notification.Emails.ToArray(),
                Subject = notification.Subject,
                Body = $"{notification.Body}"
            };

            await _sendEmailService.SendAsync(emailData);
        }
    }
}