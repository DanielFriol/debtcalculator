namespace com.debtcalculator.Domain.DTOs.Infra.Serivces
{
    public class EmailMessage
    {
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}