using Ticket.Application.Extentions;
using Ticket.Application.Models;
using Ticket.Application.Utilities;
using Ticket.Domain.Entity;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class UserService
{
    private TicketDbContext _context;
    public UserService(TicketDbContext context)
        => _context = context;


    public void AddUser(UserModel userModel)
    {

        using (var UoW = new UnitOfWork(_context))
        {

            if (UoW.UserRepository.GetByUsername(userModel.UserName) != null)
                throw new InvalidOperationException("User Already Exists");

            if (!PasswordChecker.IsSecure(userModel.Password))
                throw new Exception("Password security isnt enough");
            
            if (!userModel.Email.Contains("@gmail.com"))
                throw new ArgumentException("email invalid");

            var team = UoW.TeamRepository.GetById(userModel.TeamId);
            var user = new User(userModel.UserName, userModel.Password.ToSha256(), userModel.Email, team);

            UoW.UserRepository.Add(user);
            UoW.Commit();
        }
    }
    public IEnumerable<User> GetUsers()
    {
        using var UoW = new UnitOfWork(_context);   
        var users = UoW.UserRepository.GetAll().ToList();
        return users;
    }
    public User GetUser(int id)
    {
        using var UoW = new UnitOfWork(_context);
        var user = UoW.UserRepository.GetById(id);
        return user;
    }
}
