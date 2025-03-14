using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface IUserRepository : ICrudRepository<User>
{
    User GetByUsernameAndPassword(string username, string password);
    User GetByUsername(string username);
}
