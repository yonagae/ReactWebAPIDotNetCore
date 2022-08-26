using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinBY.Domain.Entities;

namespace FinBY.Infra.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(c => c.Id)
            .HasColumnName("Id");

        builder.Property(c => c.Name)
           .HasColumnType("varchar(100)")
           .HasColumnName("Name")
           .HasMaxLength(100)
           .IsRequired();

        builder.Property(c => c.UserName)
          .HasColumnType("varchar(50)")
          .HasColumnName("UserName")
          .HasMaxLength(100)
          .IsRequired();

        builder.Property(c => c.Password)
          .HasColumnType("varchar(130)")
          .HasColumnName("Password")
          .HasMaxLength(100)
          .IsRequired();

        builder.Property(c => c.RefreshToken)
          .HasColumnType("varchar(500)")
          .HasColumnName("RefreshToken")
          .HasMaxLength(100)
          .IsRequired();

        builder.Property(c => c.RefreshTokenExpiryTime)
          .HasColumnType("datetime")
          .HasColumnName("RefreshTokenExpiryTime")
          .HasMaxLength(100)
          .IsRequired();
    }
}
