using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private TicketDbContext _dbContext;

    public UserRepository(TicketDbContext ticketDbContext) => _dbContext = ticketDbContext;

    public void Add(User user)
    {
        _dbContext.User.Add(user);
    }
    public void Remove(User user)
    {
        _dbContext.User.Remove(user);
    }

    public void Update(User user)
    {
        _dbContext.User.Update(user);
    }

    public User GetById(int id)
    {
        var user = _dbContext.User.Include(t => t.Team).FirstOrDefault(c => c.Id == id);
        return user;
    }
    public User GetByUsernameAndPassword(string username, string password)
    {
        var user = _dbContext.User.FirstOrDefault(user => user.Username.ToLower() == username);
        return user;
    }

    public IEnumerable<User> GetAll()
    {
        var users = _dbContext.User.Include(t => t.Team).ToList();
        return users;
    }

    public User GetByUsername(string username)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Username == username.Trim());
        return user;
    }
}
