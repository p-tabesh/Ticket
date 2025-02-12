using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.Configuration;

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

        builder.Property(t => t.Action).HasComment("Add,\r\n    Edit,\r\n    Update,\r\n    Delete,\r\n    StatusChange");
    }
}
