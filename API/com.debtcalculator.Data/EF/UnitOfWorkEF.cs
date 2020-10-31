using System;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;

namespace com.debtcalculator.Data.EF
{
    public class UnitOfWorkEF : IUnitOfWork
    {
        private readonly DebtCalculatorDataContext _ctx;
        public UnitOfWorkEF(DebtCalculatorDataContext ctx) => _ctx = ctx;

        public void Commit() => _ctx.SaveChanges();

        public async Task CommitAsync() => await _ctx.SaveChangesAsync();

        public Task RollBackAsync() => throw new NotImplementedException();
    }
}