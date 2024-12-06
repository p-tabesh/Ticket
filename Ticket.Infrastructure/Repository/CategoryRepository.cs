using Microsoft.EntityFrameworkCore.Query.Internal;
using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private TicketDbContext _context;

    public CategoryRepository(TicketDbContext context)
    {
        _context = context;
    }
    public void Add(Category category)
    {
        _context.Category.Add(category);
        _context.SaveChanges();
    }

    public Category GetById(int id)
    {
        var category = _context.Category.FirstOrDefault(c => c.Id == id);
        return category;
    }
    public void Delete(Category category)
    {
        throw new NotImplementedException();
    }

    public User GetDefaultUser(int categoryId)
    {
        var defaultUser = _context.Category.FirstOrDefault(u => u.Id == categoryId);
        return defaultUser.DefaultUserAsign;
    }

    public void Update(Category category)
    {
        _context.Category.FirstOrDefault(c => c.Id == category.Id);
        _context.SaveChanges();
    }

    public void UpdateDefaultUser(int categoryId, int userId)
    {
        throw new NotImplementedException();
    }
}
