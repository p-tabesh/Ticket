using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface ITeamRepository
{
    Team GetById(int id);
    void Add(Team team);
    void Update(Team team);
    void Delete(int id);
}
