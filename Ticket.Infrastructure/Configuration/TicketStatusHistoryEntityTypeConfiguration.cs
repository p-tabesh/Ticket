using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.Configuration;

public class TicketStatusHistoryEntityTypeConfiguration : IEntityTypeConfiguration<TicketStatusHistory>
{
    public void Configure(EntityTypeBuilder<TicketStatusHistory> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Ticket)
            .WithMany(ts => ts.TicketStatusHistory)
            .HasForeignKey(t => t.TicketId);

        builder.Property(t => t.Status).HasComment("Open,\r\n    InProgress,\r\n    Finished,\r\n    Closed");
    }
}
