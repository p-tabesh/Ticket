using Ticket.Application.Mapper;
using Ticket.Application.Models;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class TeamService
{
    private TicketDbContext _dbContext;
    public TeamService(TicketDbContext dbContext) => _dbContext = dbContext;


    public IEnumerable<TeamViewModel> GetTeams()
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var teamModels = new List<TeamViewModel>();
            var teams = UoW.TeamRepository.GetAll();
            foreach (var team in teams)
            {
                var model = TeamViewMapper.MapToDto(team);
                teamModels.Add(model);
            }
            return teamModels;
        }
    }
}
