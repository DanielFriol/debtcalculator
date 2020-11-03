using System;

namespace com.debtcalculator.API.Models
{
    public class DebtSimple
    {
        public long Id { get; set; }
        public string ClientCPF { get; set; }
        public float Value { get; set; }
        public DateTime DueDate { get; set; }
        public string ContactPhone { get; set; }
        public int MaxSplit { get; set; }
        public int InterestType { get; set; }
        public float Interest { get; set; }
        public float PaschoalottoPercentage { get; set; }
        public bool Finalized { get; set; }
    }

    public static class DebtCtrlModelExtensions
    {
        public static DebtSimple ToVMSimple(this Domain.Entities.Debt debt)
        {
            return new DebtSimple
            {
                Id = debt.Id,
                ClientCPF = debt.ClientCPF,
                ContactPhone = debt.ContactPhone,
                DueDate = debt.DueDate,
                Value = debt.Value,
                MaxSplit = debt.MaxSplit,
                Interest = debt.Interest,
                InterestType = debt.InterestType,
                PaschoalottoPercentage = debt.PaschoalottoPercentage,
                Finalized = debt.Finalized
            };
        }
    }
}