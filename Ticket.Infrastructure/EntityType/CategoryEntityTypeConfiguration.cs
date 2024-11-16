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

            builder.HasMany(c => c.ChildCategories)
                .WithOne( c=> c.Parent)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.DefaultUserAsign)
                .WithMany( u=>u.Categories)
                .HasForeignKey(c=>c.DefaultUserAsignId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(f => f.Fields)
                .WithMany(c => c.Categories)
                .UsingEntity<CategoryField>();
        }
    }
}
