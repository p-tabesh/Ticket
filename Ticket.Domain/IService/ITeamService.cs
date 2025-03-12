using Ticket.Domain.Entity;

namespace Ticket.Domain.IService;


public interface ITeamService
{
    IEnumerable<Team> GetAllTeams();
    Team GetTeam(int id);
    void AddTeam(Team team);
    void RemoveTeam(int id);
}