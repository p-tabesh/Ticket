using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class TeamViewMapper
{
    public static TeamViewModel MapToDto(Team team)
    {
        var users = new List<UserViewModel>();
        foreach(var user in team.Users)
        {
            var userViewModel = UserMapper.MapToDto(user);
            users.Add(userViewModel);
        }

        var model = new TeamViewModel()
        {
            Id = team.Id,
            TeamName = team.Title,
            UsersCount = team.Users?.Count,
            Users = users
        };

        return model;
    }
}
