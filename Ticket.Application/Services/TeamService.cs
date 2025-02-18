using Ticket.Application.Mapper;
using Ticket.Application.Models;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class TeamService
{
    private TicketDbContext _dbContext;
    public TeamService(TicketDbContext dbContext) => _dbContext = dbContext;


    public IEnumerable<TeamViewModel> GetTeams(int? id)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var teamModels = new List<TeamViewModel>();

            if(id.HasValue)
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
}
