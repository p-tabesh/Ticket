using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface IFieldRepository
{
    IEnumerable<Field> GetAll();
    Field GetById(int id);
    void Add(Field field);
    void Update(Field field);
    void Remove(Field id);
}
