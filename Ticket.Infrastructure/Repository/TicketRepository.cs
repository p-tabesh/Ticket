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
        _context.Tickets.Add(ticket);
    }

    public void Update(Tickets ticket)
    {
        _context.Tickets.Update(ticket);
    }
    public Tickets GetById(int id)
    {
        var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
        return ticket;
    }
}
