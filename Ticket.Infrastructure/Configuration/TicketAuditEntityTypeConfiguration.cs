using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.EntityType;

public class TicketAuditEntityTypeConfiguration : IEntityTypeConfiguration<TicketAudit>
{
    public void Configure(EntityTypeBuilder<TicketAudit> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Ticket)
            .WithMany(ta => ta.TicketAudit)
            .HasForeignKey(t => t.TicketId);

        builder.HasOne(u => u.User)
            .WithMany(ta => ta.TicketAudits)
            .HasForeignKey(u => u.UserId);
    }
}
