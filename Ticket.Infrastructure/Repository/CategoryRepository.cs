using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;



public class CategoryRepository : ICategoryRepository
{
    private readonly TicketDbContext _context;

    public CategoryRepository(TicketDbContext context)
    {
        _context = context;
    }
    public void Add(Category category)
    {
        _context.Category.Add(category);
    }

    public Category GetById(int id)
    {
        var category = _context.Category.Include(f => f.Fields).Include(defUser => defUser.DefaultUserAsign).FirstOrDefault(c => c.Id == id);
        return category;
    }
    
    public void Delete(Category category)
    {
        // If Its Parent, childs will be deleted too
        var categories = _context.Category.Where(c => c.ParentId == category.Id || c.Id == category.Id).ToList();
        _context.Category.RemoveRange(categories);
    }
    
    public void Update(Category category)
    {
        _context.Category.FirstOrDefault(c => c.Id == category.Id);
    }
}
