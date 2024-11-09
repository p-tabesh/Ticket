using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticket.Domain.Entity;


namespace Ticket.Infrastructure.EntityType
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Parent)
                .WithMany()
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.DefaultUserAsign)
                .WithMany()
                .HasForeignKey(c=>c.DefaultUserAsignId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
