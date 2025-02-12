using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class TeamViewMapper
{
    public static TeamViewModel MapToDto(Team team)
    {
        var model = new TeamViewModel()
        {
            TeamName = team.Title,
            UsersCount = team.Users?.Count,
        };

        return model;
    }
}
