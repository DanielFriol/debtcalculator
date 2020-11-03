using System;
using MediatR;

namespace com.debtcalculator.Domain.Mediator.Debt.Add
{
    public class Request : IRequest<Response>
    {
        public string ClientCPF { get; set; }
        public float Value { get; set; }
        public DateTime DueDate { get; set; }
        public string ContactPhone { get; set; }

        public int MaxSplit { get; set; }
        public int InterestType { get; set; }
        public float Interest { get; set; }
        public float PaschoalottoPercentage { get; set; }
    }
}