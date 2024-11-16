using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.EntityType;


public class TeamEntityTypeConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(u => u.Users)
            .WithOne( t => t.Team)
            .HasForeignKey(f => f.TeamId);

    }
}
