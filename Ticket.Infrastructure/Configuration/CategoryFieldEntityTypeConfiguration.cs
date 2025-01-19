using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.Configuration;

public class CategoryFieldEntityTypeConfiguration : IEntityTypeConfiguration<CategoryField>
{
    public void Configure(EntityTypeBuilder<CategoryField> builder)
    {
        builder.HasKey(c => c.Id);


    }
}
