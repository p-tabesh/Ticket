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

    public IEnumerable<CategoryField> GetAll(int categoryId)
    {
        var categoryFields = _context.CategoryField.Include(category => category.Category).Where(f => f.CategoryId == categoryId).ToList();
        return categoryFields;
    }

    public IEnumerable<CategoryField> GetByCategoryId(int categoryId)
    {
        var categoryFields = _context.CategoryField.Include(category => category.Category)
            .Where(f => f.CategoryId == categoryId)
            .ToList();

        return categoryFields;
    }

    public IEnumerable<CategoryField> GetByFieldId(int fieldId)
    {
        throw new NotImplementedException();
    }

    public CategoryField GetByCategoryIdAndFieldId(int categoryId, int fieldId)
    {
        var categoryFields = _context.CategoryField.Include(category => category.Category)
            .FirstOrDefault(f => f.FieldId == fieldId && f.CategoryId == categoryId);
            
        return categoryFields;
    }

    public IEnumerable<CategoryField> GetAll()
    {
        var categoryField = _context.CategoryField.Include(category => category.Category).ToList();
        return categoryField;
    }
}