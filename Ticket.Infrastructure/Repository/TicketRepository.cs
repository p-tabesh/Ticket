using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entity;
using Ticket.Domain.Enums;
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
        var ticket = _context.Tickets.Include(u => u.User).Include(u => u.AssignUser).Include(c => c.Category).FirstOrDefault(t => t.Id == id);
        return ticket;
    }

    public IEnumerable<Tickets> GetWithSpecificState(Status status)
    {
        var tickets = _context.Tickets.Where(s => s.Status == status).Include(u => u.User).Include(u => u.AssignUser).Include(c => c.Category).ToList();
        return tickets;
    }
    public IEnumerable<Tickets> GetAll()
    {
        var tickets = _context.Tickets.Include(u => u.User).Include(u => u.AssignUser).Include(c => c.Category).ToList();
        return tickets;
    }
}
