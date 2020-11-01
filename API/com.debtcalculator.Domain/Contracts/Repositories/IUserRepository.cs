using System.Collections.Generic;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Entities;

namespace com.debtcalculator.Domain.Contracts.Repositories
{
    public interface IUserReadRepository : IReadRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAsync();
        Task<IEnumerable<User>> GetAsyncPaginated(int skip, int take);
        Task<User> GetAsync(long id);
        Task<User> GetByChangePasswordCodeAsync(string changePasswordCode);
        int Total { get; }
    }

    public interface IUserWriteRepository : IWriteRepository<User>
    {
        string GenerateVerificationCode();
    }
}