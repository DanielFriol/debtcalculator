namespace com.debtcalculator.Domain.Contracts.Infra
{
    public interface ILogAPI
    {
        string BodyContent { get; set; }
        void Error(string msg);
        void Warnning(string msg);
        void Info(string msg);
        void Debug(string msg);
    }
}