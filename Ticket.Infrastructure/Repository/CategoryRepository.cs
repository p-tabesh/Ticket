using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly TicketDbContext _dbContext;

    public CategoryRepository(TicketDbContext context) => _dbContext = context;

    public IEnumerable<Category> GetAll()
    {
        var categories = _dbContext.Category.Include(c => c.Parent)
            .Include(c => c.DefaultUserAsign)
            .ToList();

        return categories;
    }

    public void Add(Category category)
    {
        _dbContext.Category.Add(category);
    }

    public Category GetById(int id)
    {
        var category = _dbContext.Category.Include(f => f.Fields)
            .Include(defUser => defUser.DefaultUserAsign)
            .FirstOrDefault(c => c.Id == id);

        return category;
    }

    public void Remove(Category category)
    {
        // If Its Parent, childs will be deleted too
        var categories = _dbContext.Category.Where(c => c.ParentId == category.Id || c.Id == category.Id).ToList();

        _dbContext.Category.RemoveRange(categories);
    }

    public void Update(Category category)
    {
        _dbContext.Category.Update(category);
    }
}
