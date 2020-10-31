using com.debtcalculator.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.debtcalculator.Data.EF.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : Domain.Entities.Entity
    {
        private readonly DebtCalculatorDataContext _ctx;
        protected readonly DbSet<T> _db;
        public ReadRepository(DebtCalculatorDataContext ctx)
        {
            _ctx = ctx;
            _db = _ctx.Set<T>();
        }
    }

    public class WriteRepository<T> : IWriteRepository<T> where T : Domain.Entities.Entity
    {
        private readonly DebtCalculatorDataContext _ctx;
        protected readonly DbSet<T> _db;
        public WriteRepository(DebtCalculatorDataContext ctx)
        {
            _ctx = ctx;
            _db = _ctx.Set<T>();
        }

        public void Add(T entity)
        {
            _db.Add(entity);
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public void Update(T entity)
        {
            _db.Update(entity);
        }
    }
}