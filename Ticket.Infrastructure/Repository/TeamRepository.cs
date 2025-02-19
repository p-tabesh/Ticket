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
        _context.Team.Add(team);
    }

    public void Remove(Team team)
    {
        _context.Team.Remove(team);
    }

    public Team GetById(int id)
    {
        var team = _context.Team.Include(t => t.Users).FirstOrDefault(t => t.Id == id);
        return team;
    }

    public void Update(Team team)
    {
        _context.Team.Update(team);
    }
}
