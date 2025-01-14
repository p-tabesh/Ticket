using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.EntityType
{
    public class FieldEntityTypeConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasMany(c => c.Categories)
                .WithMany(f => f.Fields)
                .UsingEntity<CategoryField>();
            
        }
    }
}
