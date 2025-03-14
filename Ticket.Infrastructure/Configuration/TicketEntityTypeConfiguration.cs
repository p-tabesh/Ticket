using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.Configuration;

public class TicketEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entity.Ticket>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        // User who submited ticket
        builder.HasOne(u => u.SubmitUser)
            .WithMany(t => t.Tickets)
            .HasForeignKey(u => u.SubmitUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Category)
            .WithMany(t => t.Tickets)
            .HasForeignKey(c => c.CategoryId);

        // User who ticket assigned to
        builder.HasOne(ua => ua.AssignUser)
            .WithMany(t => t.AssignedTickets)
            .HasForeignKey(ua => ua.AssignUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(tn => tn.TicketNote)
            .WithOne(t => t.Ticket)
            .HasForeignKey(tn => tn.TicketId);

        builder.HasMany(ts => ts.TicketStatusHistory)
            .WithOne(t => t.Ticket)
            .HasForeignKey(ts => ts.TicketId);

        builder.HasMany(ta => ta.TicketAudit)
            .WithOne(t => t.Ticket)
            .HasForeignKey(ta => ta.TicketId);

        builder.Property(c => c.Status).HasComment("Open,\r\n    InProgress,\r\n    Finished,\r\n    Closed");

        builder.Property(c => c.Priority).HasComment("Low,\r\n    Medium,\r\n    High,\r\n    Critical");
    }
}
