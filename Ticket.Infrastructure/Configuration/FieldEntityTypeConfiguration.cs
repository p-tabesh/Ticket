using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.Configuration
{
    public class FieldEntityTypeConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasMany(c => c.Categories)
                .WithMany(f => f.Fields)
                .UsingEntity<CategoryField>();

            builder.Property(f => f.Type).HasComment("None ,\r\n    String,\r\n     Int,\r\n   Enum");
        }
    }
}
