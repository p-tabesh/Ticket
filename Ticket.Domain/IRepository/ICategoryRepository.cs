using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface ICategoryRepository
{
    void Add(Category category);
    Category GetById(int id);
    void Delete(Category category);
    void Update(Category category);
    void UpdateDefaultUser(int categoryId, int userId);
    User GetDefaultUser(int categoryId);
}
