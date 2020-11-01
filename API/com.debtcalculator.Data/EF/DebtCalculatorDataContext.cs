
using System.Threading;
using System.Threading.Tasks;
using com.debtcalculator.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace com.debtcalculator.Data.EF
{
    public class DebtCalculatorDataContext : DbContext
    {
        private readonly IConfiguration _config;
        private readonly DadosSessaoDTO _dadosSessao;
        public DebtCalculatorDataContext(IConfiguration config, DadosSessaoDTO dadosSessao)
        {
            _config = config;
            _dadosSessao = dadosSessao;
        }
        public DebtCalculatorDataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_config != null) optionsBuilder.UseMySql(_config.GetConnectionString("DebtCalcConn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Maps.UserMap());
        }

        public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }
    }
}