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

    public IEnumerable<Tickets> GetFilteredTickets(DateTime? startDate, DateTime? endDate, int? categoryId, Status? status, Priority? priority)
    {
        IQueryable<Tickets> tickets = _context.Tickets.Include(u => u.User).Include(u => u.AssignUser).Include(c => c.Category);

        if (priority.HasValue)
        {
            tickets = tickets.Where(t => t.Priority == priority);
        }

        if (status.HasValue)
        {
            tickets = tickets.Where(t => t.Status == status);
        }

        if (startDate.HasValue && endDate.HasValue)
        {
            var startId = _context.Tickets.Where(t => t.CreationDate >= startDate).OrderBy(t => t.Id).Select(t => t.Id).FirstOrDefault();
            var endId = _context.Tickets.Where(t => t.CreationDate >= endDate).OrderBy(t => t.Id).Select(t => t.Id).FirstOrDefault();

            tickets = tickets.Where(t => t.Id >= startId && t.Id <= endId);
        }

        if (startDate.HasValue && !endDate.HasValue)
        {
            tickets = tickets.Where(t => t.CreationDate >= startDate);
        }

        if (!startDate.HasValue && endDate.HasValue)
        {
            tickets = tickets.Where(t => t.CreationDate <= endDate);
        }

        if (categoryId.HasValue)
        {
            tickets = tickets.Where(t => t.CategoryId == categoryId);
        }

        return tickets.ToList();
    }


    public IEnumerable<Tickets> GetAll()
    {
        var tickets = _context.Tickets.Include(u => u.User).Include(u => u.AssignUser).Include(c => c.Category).ToList();
        return tickets;
    }
}
