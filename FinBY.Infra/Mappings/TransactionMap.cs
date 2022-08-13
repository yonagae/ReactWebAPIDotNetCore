using FinBY.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace FinBY.Infra.Mappings
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.ShortDescription)
                .HasColumnType("varchar(50)")
                .HasColumnName("ShortDescription")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Description)
                 .HasColumnType("varchar(100)")
                 .HasColumnName("Description")
                 .HasMaxLength(100);

            builder.Property(c => c.TimeStamp)
               .HasColumnType("datetime")
               .HasColumnName("TimeStamp")
               .IsRequired();

            builder.Property(c => c.TotalAmount)
           .HasColumnName("TotalAmount")
           .HasColumnType("numeric(18, 2)")
           .IsRequired();

            builder.HasOne(x => x.TransactionType);

            builder.HasMany(x => x.TransactionAmounts)
             .WithOne()
             .HasForeignKey(p => p.TransactionId)
             .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
