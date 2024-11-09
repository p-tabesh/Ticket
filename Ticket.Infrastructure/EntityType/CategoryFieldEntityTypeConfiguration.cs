using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.EntityType;

public class CategoryFieldEntityTypeConfiguration : IEntityTypeConfiguration<CategoryField>
{
    public void Configure(EntityTypeBuilder<CategoryField> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Category)
            .WithMany()
            .HasForeignKey(c => c.Id);

        builder.HasOne(f => f.Field)
            .WithMany()
            .HasForeignKey(f => f.FieldId);
    }
}
