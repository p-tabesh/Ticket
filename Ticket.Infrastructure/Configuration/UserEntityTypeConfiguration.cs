﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(u => u.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TeamId);


            builder.HasMany(c => c.Categories)
                .WithOne(u => u.DefaultUserAsign)
                .HasForeignKey(u => u.DefaultUserAsignId);

            // user who submited ticket
            builder.HasMany(t => t.Tickets)
                .WithOne(t => t.SubmitUser)
                .HasForeignKey(u => u.SubmitUserId);

            // user who ticket assigned
            builder.HasMany(t => t.AssignedTickets)
                .WithOne(u => u.AssignUser)
                .HasForeignKey(u => u.AssignUserId);

            builder.HasMany(tn => tn.Notes)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            builder.HasMany(ta => ta.TicketAudits)
                .WithOne(ta => ta.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
