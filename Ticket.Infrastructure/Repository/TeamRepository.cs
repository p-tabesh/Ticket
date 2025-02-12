using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class TeamRepository : ITeamRepository
{
    private TicketDbContext _context;
    public TeamRepository(TicketDbContext context) => _context = context;

    public IEnumerable<Team> GetAll()
    {
        var teams = _context.Team.Include(t => t.Users).ToList();
        return teams;
    }
    public void Add(Team team)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Team GetById(int id)
    {
        var team = _context.Team.FirstOrDefault(t => t.Id == id);
        return team;
    }

    public void Update(Team team)
    {
        throw new NotImplementedException();
    }
}
