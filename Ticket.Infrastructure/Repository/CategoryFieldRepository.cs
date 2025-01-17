using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class CategoryFieldRepository : ICategoryFieldRepository
{
    private readonly TicketDbContext _context;
    public CategoryFieldRepository(TicketDbContext context)
    {
        _context = context;
    }

    public void Add(CategoryField categoryField)
    {
        _context.CategoryField.Add(categoryField);
    }

    public void Remove(CategoryField categoryField)
    {
        _context.CategoryField.Remove(categoryField);
    }

    public IEnumerable<CategoryField> GetFields(int categoryId)
    {
        var categoryFields = _context.CategoryField.Include(field => field.Category).Where(f => f.CategoryId == categoryId).ToList();
        return categoryFields;
    }
}