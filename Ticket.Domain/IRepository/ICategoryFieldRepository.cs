using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface ICategoryFieldRepository
{
    void Add(CategoryField categoryField);
    void Remove(CategoryField categoryField);

}
