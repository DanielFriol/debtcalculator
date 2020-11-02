using System;

namespace com.debtcalculator.API.Models
{
    public class DebtSimple
    {
        public string ClientCPF { get; set; }
        public float Value { get; set; }
        public DateTime DueDate { get; set; }
        public string ContactPhone { get; set; }
    }

    public static class DebtCtrlModelExtensions
    {
        public static DebtSimple ToVMSimple(this Domain.Entities.Debt debt)
        {
            return new DebtSimple
            {
                ClientCPF = debt.ClientCPF,
                ContactPhone = debt.ContactPhone,
                DueDate = debt.DueDate,
                Value = debt.Value
            };
        }
    }
}