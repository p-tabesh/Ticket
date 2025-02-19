using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private TicketDbContext _ticketDbContext;
    public UserRepository(TicketDbContext ticketDbContext)
    {
        _ticketDbContext = ticketDbContext;
    }

    public void Add(User user)
    {
        _ticketDbContext.Users.Add(user);
    }

    public User GetById(int id)
    {
        try
        {
            var user = _ticketDbContext.Users.Include(t => t.Team).FirstOrDefault(c => c.Id == id);
            return user;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public User GetByUsername(string username)
    {
        var user = _ticketDbContext.Users.FirstOrDefault(user => user.Username == username);
        return user;
    }

    public IEnumerable<User> GetAll()
    {
        var users = _ticketDbContext.Users.Include(t => t.Team).ToList();
        return users;
    }

    public void Remove(User user)
    {
        _ticketDbContext.Users.Remove(user);
    }

    public void Update(User user)
    {
        _ticketDbContext.Users.Update(user);
    }
}
