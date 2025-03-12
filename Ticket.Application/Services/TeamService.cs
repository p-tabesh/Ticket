using Ticket.Domain.Entity;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Domain.IService;
namespace Ticket.Application.Services;

public class TeamService : ITeamService
{
    private TicketDbContext _dbContext;
    public TeamService(TicketDbContext dbContext) => _dbContext = dbContext;


    //public IEnumerable<TeamViewModel> GetTeams(int? id = null)
    //{
    //    using (var UoW = new UnitOfWork(_dbContext))
    //    {
    //        var teamModels = new List<TeamViewModel>();

    //        if (id.HasValue)
    //        {
    //            var team = UoW.TeamRepository.GetById(id.Value);
    //            if (team == null)
    //                throw new Exception("team doesnt exists");

    //            var teamModel = TeamViewMapper.MapToDto(team);
    //            teamModels.Add(teamModel);
    //            return teamModels;
    //        }

    //        var teams = UoW.TeamRepository.GetAll();
    //        foreach (var team in teams)
    //        {
    //            var teamModel = TeamViewMapper.MapToDto(team);
    //            teamModels.Add(teamModel);
    //        }
    //        return teamModels;
    //    }
    //}

    //public void Add(AddTeamModel addTeamModel)
    //{
    //    if (string.IsNullOrEmpty(addTeamModel.Name))
    //        throw new Exception("title invalid");

    //    using (var UoW = new UnitOfWork(_dbContext))
    //    {
    //        var allTeams = UoW.TeamRepository.GetAll();

    //        if (allTeams.Any(t => addTeamModel.Name.Trim() == t.Title))
    //            throw new Exception("another team with this name already exists");

    //        var team = new Team(addTeamModel.Name);
    //        UoW.TeamRepository.Add(team);
    //        UoW.Commit();
    //    }
    //}

    //public void Remove(RemoveTeamModel removeTeamModel)
    //{
    //    using (var UoW = new UnitOfWork(_dbContext))
    //    {
    //        var team = UoW.TeamRepository.GetById(removeTeamModel.Id);
    //        if (team == null)
    //            throw new Exception("team doesnt exists");

    //        UoW.TeamRepository.Remove(team);
    //        UoW.Commit();
    //    }
    //}


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
