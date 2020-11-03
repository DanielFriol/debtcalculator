using MediatR;

namespace com.debtcalculator.Domain.Mediator.Debt.UpdateConfig
{
    public class Request : IRequest<Response>
    {
        public long Id { get; set; }
        public int MaxSplit { get; set; }
        public int InterestType { get; set; }
        public float Interest { get; set; }
        public float PaschoalottoPercentage { get; set; }
    }
}