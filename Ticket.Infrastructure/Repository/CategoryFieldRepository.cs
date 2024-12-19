using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class CategoryFieldRepository:ICategoryFieldRepository
{
    private readonly TicketDbContext _context;
    public CategoryFieldRepository(TicketDbContext context)
    {
        _context = context;
    }

    public void Add(CategoryField categoryField)
    {
        _context.CategoryField.Add(categoryField);
        _context.SaveChanges();
    }

    public void Remove(CategoryField categoryField)
    {
        throw new NotImplementedException();
    }
}
