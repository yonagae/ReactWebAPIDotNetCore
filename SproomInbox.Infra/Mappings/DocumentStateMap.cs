using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SproomInbox.Domain.Entities;

namespace SproomInbox.Infra.Mappings
{
    public class DocumentStateMap : IEntityTypeConfiguration<DocumentState>
    {
        public void Configure(EntityTypeBuilder<DocumentState> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.TimeStamp)
                .HasColumnName("TimeStamp")
                .IsRequired();

            builder.Property(c => c.State)
            .HasColumnName("State")
            .IsRequired();

            builder.HasOne(x => x.Document);
        }
    }
}
