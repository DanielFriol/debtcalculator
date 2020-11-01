using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra.Services;
using com.debtcalculator.Domain.DTOs.Infra.Serivces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace com.debtcalculator.Services.Notification.Email
{
    public class SendEmailService : ISendEmailService
    {
        private readonly SendGridClient _sendGridCLient;

        public SendEmailService(IConfiguration config)
        {
            var sendGridKey = config.GetValue<string>("SendGridAPIKey");
            _sendGridCLient = new SendGridClient(sendGridKey);
        }

        public async Task SendAsync(EmailMessage data)
        {
            var sendGridMessage = new SendGridMessage()
            {
                From = new EmailAddress("debtcalculator@daniel.com.br", "Debt Calculator"),
                Subject = data.Subject,
                HtmlContent =
                    "<html>" +
                    "<head></head>" +
                    "<body style=\"font-family: Arial, sans-serif, 'Lato'; font-size: 14px;\">" +
                    $"{data.Body}" +
                    "<br>" +
                    "<br>" +
                    $"<p>Copyright © {DateTime.UtcNow.Year}</p>" +
                    "</body>" +
                    "</html>"
            };

            var emailsAddress = new List<EmailAddress>();
            foreach (var email in data.To)
            {
                emailsAddress.Add(new EmailAddress(email));
            }
            sendGridMessage.AddTos(emailsAddress);

            await _sendGridCLient.SendEmailAsync(sendGridMessage).ConfigureAwait(false);
        }
    }
}