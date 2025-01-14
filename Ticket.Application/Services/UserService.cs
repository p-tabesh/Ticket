using Microsoft.IdentityModel.Tokens;
using Ticket.Application.Models;
using Ticket.Application.Utilities;
using Ticket.Domain.Entity;
using Ticket.Domain.Exceptions;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class UserService
{
    private TicketDbContext _context;
    private IUserRepository _userRepository;
    private ITeamRepository _tenantRepository;
    public UserService(TicketDbContext context, IUserRepository userRepository, ITeamRepository teamRepository)
    {
        _context = context;
        _userRepository = userRepository;
        _tenantRepository = teamRepository;
    }

    public void AddUser(UserModel userModel)
    {

        using (var UoW = new UnitOfWork(_context))
        {

            if (_userRepository.GetByUsername(userModel.UserName) != null)
            {
                UoW.Rollback();
                throw new InvalidOperationException("User Already Exists");
            }
            if (!PasswordChecker.IsSecure(userModel.Password))
            {
                UoW.Rollback();
                throw new Exception("Password security isnt enough");
            }
            if (!userModel.Email.Contains("@gmail.com"))
            {
                throw new ArgumentException("email invalid");
            }

            var team = _tenantRepository.GetById(userModel.TeamId);

            var user = new User(userModel.UserName, userModel.Password, userModel.Email, team);
            _userRepository.Add(user);
            UoW.Commit();
        }
    }
    public IEnumerable<User> GetUsers()
    {
        var users = _userRepository.GetAll().ToList();
        return users;
    }
    public User GetUser(int id)
    {
        var user = _userRepository.GetById(id);
        return user;
    }
}
