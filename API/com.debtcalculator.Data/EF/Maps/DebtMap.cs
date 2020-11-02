using com.debtcalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace com.debtcalculator.Data.EF.Maps
{
    public class DebtMap : IEntityTypeConfiguration<Debt>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Debt> builder)
        {
            builder.ToTable(nameof(Debt));

            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.ClientCPF).HasColumnType("varchar(11)");
            builder.Property(p => p.Value).HasColumnType("float");
            builder.Property(p => p.DueDate).HasColumnType("datetime");
            builder.Property(p => p.ContactPhone).HasColumnType("varchar(30)");
            builder.Property(p => p.Finalized).HasColumnType("tinyint(4)");
            builder.Property(p => p.FinalizedDate).HasColumnType("date");
            builder.Property(p => p.PaschoalottoValue).HasColumnType("float");
            builder.Property(p => p.Interest).HasColumnType("float");
            builder.Property(p => p.PaschoalottoPercentage).HasColumnType("float");
            builder.Property(p => p.InterestType).HasColumnType("int(2)");
            builder.Property(p => p.MaxSplit).HasColumnType("int");


        }
    }
}