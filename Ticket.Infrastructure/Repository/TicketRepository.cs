using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;
namespace Ticket.Infrastructure.Repository;

public class TicketRepository : ITicketRepository
{
    private readonly TicketDbContext _context;
    public TicketRepository(TicketDbContext context)
    {
        _context = context;
    }
    public void Add(Tickets ticket)
    {
        _context.Add(ticket);
    }
}
