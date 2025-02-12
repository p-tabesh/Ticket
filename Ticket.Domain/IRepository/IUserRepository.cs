using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface IUserRepository
{
    void Add(User user);
    User GetById(int id);
    User GetByUsername(string username);
    IEnumerable<User> GetAll();
}
