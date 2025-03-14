using Ticket.Domain.Entity;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Domain.IService;
namespace Ticket.Application.Services;

public class TeamService : ITeamService
{
    private TicketDbContext _dbContext;
    public TeamService(TicketDbContext dbContext) => _dbContext = dbContext;
    public void AddTeam(Team team)
    {
        if (string.IsNullOrEmpty(team.Title))
            throw new Exception("title invalid");

        using (var UoW = new UnitOfWork(_dbContext))
        {
            var allTeams = UoW.TeamRepository.GetAll();
            UoW.TeamRepository.Add(team);
            UoW.Commit();
        }
    }

    public void RemoveTeam(int id)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var team = UoW.TeamRepository.GetById(id);
            if (team == null)
                throw new Exception("Team doesnt exists");

            UoW.TeamRepository.Remove(team);
            UoW.Commit();
        }
    }

    public IEnumerable<Team> GetAllTeams()
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var teams = UoW.TeamRepository.GetAll();
            return teams;
        }
    }

    public Team GetTeam(int id)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var team = UoW.TeamRepository.GetById(id);
            if (team == null)
                throw new Exception("Team doesn't exists");

            return team;
        }
    }
}
