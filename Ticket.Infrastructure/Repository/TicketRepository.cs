using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Enums;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class TicketRepository : ITicketRepository
{
    private readonly TicketDbContext _dbContext;

    public TicketRepository(TicketDbContext context) => _dbContext = context;

    public void Add(Domain.Entity.Ticket ticket)
    {
        _dbContext.Ticket.Add(ticket);
    }

    public void Update(Domain.Entity.Ticket ticket)
    {
        _dbContext.Ticket.Update(ticket);
    }

    public Domain.Entity.Ticket GetById(int id)
    {
        var ticket = _dbContext.Ticket
            .Include(u => u.SubmitUser)
            .Include(u => u.AssignUser)
            .Include(c => c.Category)
            .Include(n => n.TicketNote).ThenInclude(u => u.User)
            .Include(a => a.TicketAudit).ThenInclude(u => u.User)
            .FirstOrDefault(t => t.Id == id);

        return ticket;
    }

    public IEnumerable<Domain.Entity.Ticket> GetWithFilters(DateTime? startDate, DateTime? endDate, int? categoryId, Status? status, Priority? priority)
    {
        IQueryable<Domain.Entity.Ticket> tickets = _dbContext.Ticket.Include(u => u.SubmitUser)
            .Include(u => u.AssignUser)
            .Include(c => c.Category);

        if (priority.HasValue)
        {
            tickets = tickets.Where(t => t.Priority == priority);
        }

        if (status.HasValue)
        {
            tickets = tickets.Where(t => t.Status == status);
        }

        if (startDate.HasValue)
        {
            tickets = tickets.Where(t => t.CreationDate >= startDate);
        }

        if (endDate.HasValue)
        {
            tickets = tickets.Where(t => t.CreationDate <= endDate);
        }

        if (categoryId.HasValue)
        {
            tickets = tickets.Where(t => t.CategoryId == categoryId);
        }

        return tickets.ToList();
    }

    public IEnumerable<Domain.Entity.Ticket> GetAll()
    {
        var tickets = _dbContext.Ticket
            .Include(u => u.SubmitUser)
            .Include(u => u.AssignUser)
            .Include(c => c.Category)
            .Include(n => n.TicketNote).ThenInclude(u => u.User)
            .Include(a => a.TicketAudit).ThenInclude(u => u.User).Take(100)
            .ToList();

        return tickets;
    }

    public void Remove(Domain.Entity.Ticket entity)
    {
        // No Need To Remove
        throw new NotImplementedException();
    }
}
