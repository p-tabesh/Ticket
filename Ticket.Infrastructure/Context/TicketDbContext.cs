using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entity;
using Ticket.Infrastructure.EntityType;

namespace Ticket.Infrastructure.Context
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<CategoryField> CategoryField { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<TicketAudit> TicketAudit { get; set; }
        public DbSet<TicketStatusHistory> TicketStatusHistory { get; set; }
        public DbSet<TicketNote> TicketNote { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FieldEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryFieldEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TeamEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketAuditEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketStatusHistoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketNoteEntityTypeConfiguration());
        }
    }
}
