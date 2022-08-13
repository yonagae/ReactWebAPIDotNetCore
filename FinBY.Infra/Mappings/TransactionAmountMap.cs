
using FinBY.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace FinBY.Infra.Mappings
{
    public class TransactionAmountMap : IEntityTypeConfiguration<TransactionAmount>
    {
        public void Configure(EntityTypeBuilder<TransactionAmount> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Amount)
            .HasColumnName("Amount")
            .HasColumnType("numeric(18, 2)")
            .IsRequired();

            builder.HasOne(x => x.User);

            //builder.HasOne(x => x.Transaction)
            //    .WithMany(c => c.TransactionAmounts)
            //    .IsRequired();
        }
    }
}

