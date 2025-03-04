using Ticket.Application.Mapper;
using Ticket.Application.Models;
using Ticket.Domain.Entity;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class TeamService
{
    private TicketDbContext _dbContext;
    public TeamService(TicketDbContext dbContext) => _dbContext = dbContext;


    public IEnumerable<TeamViewModel> GetTeams(int? id = null)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var teamModels = new List<TeamViewModel>();

            if (id.HasValue)
            {
                var team = UoW.TeamRepository.GetById(id.Value);
                if (team == null)
                    throw new Exception("team doesnt exists");

                var teamModel = TeamViewMapper.MapToDto(team);
                teamModels.Add(teamModel);
                return teamModels;
            }

            var teams = UoW.TeamRepository.GetAll();
            foreach (var team in teams)
            {
                var teamModel = TeamViewMapper.MapToDto(team);
                teamModels.Add(teamModel);
            }
            return teamModels;
        }
    }

    public void Add(AddTeamModel addTeamModel)
    {
        if (string.IsNullOrEmpty(addTeamModel.Name))
            throw new Exception("title invalid");

        using (var UoW = new UnitOfWork(_dbContext))
        {
            var allTeams = UoW.TeamRepository.GetAll();

            if (allTeams.Any(t => addTeamModel.Name.Trim() == t.Title))
                throw new Exception("another team with this name already exists");

            var team = new Team(addTeamModel.Name);
            UoW.TeamRepository.Add(team);
            UoW.Commit();
        }

    }

    public void Remove(RemoveTeamModel removeTeamModel)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var team = UoW.TeamRepository.GetById(removeTeamModel.Id);
            if (team == null)
                throw new Exception("team doesnt exists");

            UoW.TeamRepository.Remove(team);
            UoW.Commit();
        }

    }
}
