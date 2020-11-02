using com.debtcalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace com.debtcalculator.Data.EF.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasColumnType("varchar(80)");
            builder.Property(p => p.Email).HasColumnType("varchar(80)");
            builder.Property(p => p.IdProfile).HasColumnType("bigint(20)");
            builder.Property(p => p.Password).HasColumnType("varchar(120)");
            builder.Property(p => p.ChangePasswordCode).HasColumnType("varchar(120)");
            builder.Property(p => p.Salt).HasColumnType("varchar(150)");
            builder.Property(p => p.CodeExpiration).HasColumnType("date");
            builder.Property(p => p.CPF).HasColumnType("varchar(11)");

            
        }
    }
}