using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.EntityType;

public class TicketEntityTypeConfiguration : IEntityTypeConfiguration<Tickets>
{
    public void Configure(EntityTypeBuilder<Tickets> builder)
    {
        builder.HasKey(t => t.Id);

        // User who submited ticket
        builder.HasOne(u => u.User)
            .WithMany(t => t.Tickets)
            .HasForeignKey(u => u.UserId)
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
    }
}
