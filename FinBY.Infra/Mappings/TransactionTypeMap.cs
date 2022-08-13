using FinBY.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace FinBY.Infra.Mappings
{
    public class TransactionTypeMap : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
