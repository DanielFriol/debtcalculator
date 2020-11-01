using System.Collections.Generic;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.User.AddAdmin
{
    public class Notification : INotification
    {
        public Notification(string subject, string body, List<string> emails)
        {
            Subject = subject;
            Body = body;
            Emails = emails;
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Emails { get; set; }
    }

}