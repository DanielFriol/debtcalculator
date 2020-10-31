using com.debtcalculator.Domain.Entities;

namespace com.debtcalculator.Domain.Contracts.Repositories
{
    public interface IReadRepository<T> where T : Entity
    {
        
    }

    public interface IWriteRepository<T> where T : Entity
    {
        void Add(T entity);
        void Update (T entity);
        void Delete (T entity);
    }
}