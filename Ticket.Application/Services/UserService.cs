using Ticket.Application.Extentions;
using Ticket.Application.Mapper;
using Ticket.Application.Models;
using Ticket.Application.Utilities;
using Ticket.Domain.Entity;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class UserService
{
    private TicketDbContext _dbContext;
    public UserService(TicketDbContext context) => _dbContext = context;

    public void AddUser(UserModel userModel)
    {

        using (var UoW = new UnitOfWork(_dbContext))
        {

            if (UoW.UserRepository.GetByUsername(userModel.UserName) != null)
                throw new InvalidOperationException("User Already Exists");

            if (!PasswordChecker.IsSecure(userModel.Password.ToSha256()))
                throw new Exception("Password security isnt enough");

            if (!userModel.Email.Contains("@gmail.com"))
                throw new ArgumentException("email invalid");

            var team = UoW.TeamRepository.GetById(userModel.TeamId);
            var user = new User(userModel.UserName, userModel.Password.ToSha256(), userModel.Email, team);

            UoW.UserRepository.Add(user);
            UoW.Commit();
        }
    }
    public IEnumerable<UserViewModel> GetAllUsers()
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var userModels = new List<UserViewModel>();
            var users = UoW.UserRepository.GetAll().ToList();
            foreach (var user in users)
            {
                var model = UserMapper.MapToDto(user);
                userModels.Add(model);
            }
            return userModels;
        }
    }
    public User GetUser(int id)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var user = UoW.UserRepository.GetById(id);
        return user;
    }

    public void ChangeUsername(int id, string newUsername)
    {
        using var UoW = new UnitOfWork(_dbContext);

        var users = UoW.UserRepository.GetAll();
        if (users.Any(u => u.Username == newUsername))
            throw new Exception("another user with this username exists");

        var user = UoW.UserRepository.GetById(id);

        if (user == null)
            throw new Exception("user doesnt exists");

        user.ChangeUsername(newUsername);
        UoW.UserRepository.Update(user);
        UoW.Commit();
    }

    public void ChangePassword(string newPassword)
    {
        
    }

    public void ActiveUser(int userId)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var user = UoW.UserRepository.GetById(userId);
        user.Active();
        UoW.UserRepository.Update(user);
        UoW.Commit();
    }

    public void DeActiveUser(int userId)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var user = UoW.UserRepository.GetById(userId);
        user.DeActive();
        UoW.UserRepository.Update(user);
        UoW.Commit();
    }

    public void ChangeTeam(int newTeamId, int userId)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var user = UoW.UserRepository.GetById(userId);
        var team = UoW.TeamRepository.GetById(newTeamId);
        user.ChangeTeam(team.Id);
        UoW.UserRepository.Update(user);
        UoW.Commit();
    }
}
