using System;

namespace com.debtcalculator.Domain.Entities
{
    public class Debt : Entity
    {
        public Debt(string clientCPF, float value, DateTime dueDate, string contactPhone, int maxSplit, int interestType, float interest, float paschoalottoPercentage)
        {
            ClientCPF = clientCPF;
            Value = value;
            DueDate = dueDate;
            ContactPhone = contactPhone;
            MaxSplit = maxSplit;
            InterestType = interestType;
            Interest = interest;
            PaschoalottoPercentage = paschoalottoPercentage;
            Finalized = false;
            FinalizedDate = null;
        }


        public void UpdateConfig(int maxSplit, int interestType, float interest, float paschoalottoPercentage)
        {
            MaxSplit = maxSplit;
            InterestType = interestType;
            Interest = interest;
            PaschoalottoPercentage = paschoalottoPercentage;
        }

        public void FinalizeDebt()
        {
            Finalized = true;
            FinalizedDate = DateTime.UtcNow;
        }

        public void UpdatePaschoalottoValue(float value)
        {
            PaschoalottoValue = value;
        }

        public string ClientCPF { get; set; }
        public float Value { get; set; }
        public DateTime DueDate { get; set; }
        public string ContactPhone { get; set; }
        public bool Finalized { get; set; }
        public DateTime? FinalizedDate { get; set; }
        public float PaschoalottoValue { get; set; }
        public int MaxSplit { get; set; }
        public int InterestType { get; set; }
        public float Interest { get; set; }
        public float PaschoalottoPercentage { get; set; }
    }
}