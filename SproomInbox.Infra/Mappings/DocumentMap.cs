using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Enum;

namespace SproomInbox.Infra.Mappings
{
    public class DocumentMap : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.FileReference)
                .HasColumnType("varchar(100)")
                .HasColumnName("FileReference")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.DocumentType)
                .HasColumnName("DocumentType")
                .IsRequired();

            builder.Property(c => c.State)
             .HasColumnName("State")
             .IsRequired();
        }
    }
}
