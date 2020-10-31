using System.Threading.Tasks;

namespace com.debtcalculator.Domain.Contracts.Infra
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
        Task RollBackAsync();
    }
}