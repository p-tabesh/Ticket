using System;
using Ticket.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ticket.Infrastructure.EntityType;

public class TicketStatusHistoryEntityTypeConfiguration : IEntityTypeConfiguration<TicketStatusHistory>
{
    public void Configure(EntityTypeBuilder<TicketStatusHistory> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Ticket)
            .WithMany(ts => ts.TicketStatusHistory)
            .HasForeignKey(t => t.TicketId);
    }
}
