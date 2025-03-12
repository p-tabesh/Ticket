using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class TeamMapper
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

    public static IEnumerable<TeamViewModel> MapToDto(IEnumerable<Team> teams)
    {
        var users = new List<UserViewModel>();
        var teamsModel = new List<TeamViewModel>();

        foreach (var team in teams)
        {
            foreach (var user in team?.Users)
            {
                var userViewModel = UserMapper.MapToDto(user);
                users.Add(userViewModel);
            }
            teamsModel.Add(new TeamViewModel()
            {
                Id = team.Id,
                TeamName = team.Title,
                Users = users,
                UsersCount = users.Count
            });
        }

        return teamsModel;
    }

    public static Team MapToEntity(AddTeamModel model)
    {
        var team = new Team(model.Name);
        return team;
    }
}
