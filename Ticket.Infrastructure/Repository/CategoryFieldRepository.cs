using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class CategoryFieldRepository : ICategoryFieldRepository
{
    private readonly TicketDbContext _dbContext;

    public CategoryFieldRepository(TicketDbContext context) => _dbContext = context;

    public void Add(CategoryField categoryField)
    {
        _dbContext.CategoryField.Add(categoryField);
    }

    public void Remove(CategoryField categoryField)
    {
        _dbContext.CategoryField.Remove(categoryField);
    }

    public void Update(CategoryField entity)
    {
        _dbContext.CategoryField.Update(entity);
    }

    public IEnumerable<CategoryField> GetAll(int categoryId)
    {
        var categoryFields = _dbContext.CategoryField.Include(category => category.Category)
            .Where(f => f.CategoryId == categoryId)
            .ToList();

        return categoryFields;
    }

    public IEnumerable<CategoryField> GetByCategoryId(int categoryId)
    {
        var categoryFields = _dbContext.CategoryField.Include(category => category.Category)
            .Where(f => f.CategoryId == categoryId)
            .ToList();

        return categoryFields;
    }

    public IEnumerable<CategoryField> GetByFieldId(int fieldId)
    {
        var categoryFields = _dbContext.CategoryField.Include(category => category.Category)
            .Where(f => f.FieldId == fieldId)
            .ToList();

        return categoryFields;
    }

    public CategoryField GetByCategoryIdAndFieldId(int categoryId, int fieldId)
    {
        var categoryFields = _dbContext.CategoryField.Include(category => category.Category)
            .FirstOrDefault(f => f.FieldId == fieldId && f.CategoryId == categoryId);

        return categoryFields;
    }

    public IEnumerable<CategoryField> GetAll()
    {
        var categoryField = _dbContext.CategoryField.Include(category => category.Category).ToList();
        return categoryField;
    }

    public CategoryField GetById(int id)
    {
        var categoryField = _dbContext.CategoryField.FirstOrDefault(cf => cf.Id == id);
        return categoryField;
    }
}