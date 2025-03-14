using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class TeamMapper
{
    public static TeamViewModel MapToDto(Team team)
    {
        var users = new List<UserViewModel>();

        foreach (var user in team.Users)
        {
            var userViewModel = UserMapper.MapToDto(user);
            users.Add(userViewModel);
        }

        var model = new TeamViewModel(team.Id, team.Title, team.Users?.Count, users);

        return model;
    }

    public static IEnumerable<TeamViewModel> MapToDto(IEnumerable<Team> teams)
    {
        var users = new List<UserViewModel>();

        foreach (var team in teams)
        {
            users.Clear();
            foreach (var user in team.Users)
            {
                var userViewModel = UserMapper.MapToDto(user);
                users.Add(userViewModel);
            }

            yield return new TeamViewModel(team.Id, team.Title, users.Count, users);
        }
    }

    public static Team MapToEntity(AddTeamModel model)
    {
        var team = new Team(model.Name);
        return team;
    }
}
