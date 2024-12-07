using System;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.EntityType;

public class TicketNoteEntityTypeConfiguration : IEntityTypeConfiguration<TicketNote>
{
    public void Configure(EntityTypeBuilder<TicketNote> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Ticket)
            .WithMany(tn => tn.TicketNote)
            .HasForeignKey(t => t.TicketId);

        builder.HasOne(u => u.User)
            .WithMany(tn => tn.Notes)
            .HasForeignKey(u => u.UserId);
    }
}
