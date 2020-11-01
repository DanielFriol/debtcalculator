using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.Entities;
using com.debtcalculator.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace com.debtcalculator.Data.EF.Repositories
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        public UserReadRepository(DebtCalculatorDataContext ctx) : base(ctx)
        {
        }

        public int Total { get; private set; }

        public async Task<User> GetAsync(long id)
        {
            return await _db.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAsyncPaginated(int skip, int take)
        {
            var query = _db.AsQueryable();
            Total = _db.Count();
            query = query.Skip(skip).Take(take).OrderBy(x => x.Name);
            return await query.ToListAsync();
        }

        public async Task<User> GetByChangePasswordCodeAsync(string changePasswordCode)
        {
            return await _db.FirstOrDefaultAsync(x => x.ChangePasswordCode == changePasswordCode.Encrypt(x.Salt));
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _db.FirstOrDefaultAsync(x => x.Email == email);
        }
    }

    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        public UserWriteRepository(DebtCalculatorDataContext ctx) : base(ctx)
        {
        }

        public string GenerateVerificationCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}