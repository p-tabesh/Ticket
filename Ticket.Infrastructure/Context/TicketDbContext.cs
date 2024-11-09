using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using Ticket.Domain.Entity;

namespace Ticket.Infrastructure.Context
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<TicketAudit> TicketAudit { get; set; }
        public DbSet<TicketStatusHistory> TicketStatusHistory { get; set; }
        public DbSet<TicketNote> TicketNote { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
