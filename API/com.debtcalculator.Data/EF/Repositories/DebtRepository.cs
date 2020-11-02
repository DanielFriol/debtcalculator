using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace com.debtcalculator.Data.EF.Repositories
{
    public class DebtReadRepository : ReadRepository<Debt>, IDebtReadRepository
    {
        public DebtReadRepository(DebtCalculatorDataContext ctx) : base(ctx)
        {
        }

        public int Total { get; private set; }

        public async Task<IEnumerable<Debt>> GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<IEnumerable<Debt>> GetAllPaginated(int skip, int take)
        {
            var query = _db.AsQueryable();
            Total = query.Count();
            query = _db.Skip(skip).Take(take);
            query = query.OrderBy(x => x.DueDate);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Debt>> GetAllPaginatedByCPF(string cpf, int skip, int take)
        {
            var query = _db.AsQueryable();
            query = query.Where(x=> x.ClientCPF.Equals(cpf));
            Total = query.Count();
            query = query.Skip(skip).Take(take);
            query = query.OrderBy(x => x.DueDate);
            return await query.ToListAsync();
        }

        public async Task<Debt> GetAsync(long id)
        {
           return await _db.FirstOrDefaultAsync(x=> x.Id == id);
        }
    }

    public class DebtWriteRepository : WriteRepository<Debt>, IDebtWriteRepository
    {
        public DebtWriteRepository(DebtCalculatorDataContext ctx) : base(ctx)
        {
        }
    }
}