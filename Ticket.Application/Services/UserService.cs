
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
    public UserService(TicketDbContext context)
    {
         _dbContext = context;
    }

    public void AddUser(UserModel userModel)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {

            if (UoW.UserRepository.GetByUsername(userModel.UserName) != null)
                throw new InvalidOperationException("User Already Exists");

            if (!PasswordChecker.IsSecure(userModel.Password))
                throw new Exception("Password security isnt enough");

            if (!userModel.Email.Contains("@gmail.com"))
                throw new ArgumentException("email invalid");

            var team = UoW.TeamRepository.GetById(userModel.TeamId);
            if (team == null)
                throw new Exception("team doesnt exists");

            var user = new User(userModel.UserName, userModel.Password.ToSha256(), userModel.Email, userModel.TeamId, userModel.IsAdmin);

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

    public UserViewModel GetUser(int id)
    {
        using (var UoW = new UnitOfWork (_dbContext))
        {
            var user = UoW.UserRepository.GetById(id);

            if (user == null)
                throw new Exception("User doesn't exists");

            var model = UserMapper.MapToDto(user);
            return model;
        }
    }

    public void ChangeUsername(int id, string newUsername)
    {
        using var UoW = new UnitOfWork(_dbContext);

        var users = UoW.UserRepository.GetAll();
        if (users.Any(u => u.Username.ToLower() == newUsername.ToLower()))
            throw new Exception("another user with this username exists");

        var user = UoW.UserRepository.GetById(id);

        if (user == null)
            throw new Exception("user doesnt exists");

        user.ChangeUsername(newUsername);
        UoW.UserRepository.Update(user);
        UoW.Commit();
    }

    public void ChangePassword(int userId, string newPassword)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var user = UoW.UserRepository.GetById(userId);

        if (user == null)
            throw new Exception("user doesnt exists");

        if (user.Password == newPassword.ToSha256())
            throw new Exception("Cannot choose current password");

        if (!PasswordChecker.IsSecure(newPassword))
            throw new Exception("password security rate is not enough");

        user.ChangePassword(newPassword.ToSha256());
        UoW.UserRepository.Update(user);
        UoW.Commit();
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

    public void PromoteUser(PromoteUserModel model)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var user = UoW.UserRepository.GetById(model.UserId);
        user.Promote();
        UoW.UserRepository.Update(user);
        UoW.Commit();
    }

    public void Demote(DemoteUserModel model)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var user = UoW.UserRepository.GetById(model.UserId);
        user.Demote();
        UoW.UserRepository.Update(user);
        UoW.Commit();
    }
}


