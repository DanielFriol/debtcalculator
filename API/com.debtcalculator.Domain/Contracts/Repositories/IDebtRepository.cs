using System.Collections.Generic;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Entities;

namespace com.debtcalculator.Domain.Contracts.Repositories
{
    public interface IDebtReadRepository : IReadRepository<Debt>
    {
        Task<Debt> GetAsync(long id);
        Task<IEnumerable<Debt>> GetAllAsync();
        Task<IEnumerable<Debt>> GetAllPaginated(int skip, int take);
        Task<IEnumerable<Debt>> GetAllPaginatedByCPF(string cpf, int skip, int take);

        int Total { get; }


    }
    public interface IDebtWriteRepository : IWriteRepository<Debt>
    {

    }
}