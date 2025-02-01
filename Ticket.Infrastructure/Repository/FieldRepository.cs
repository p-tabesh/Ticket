using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class FieldRepository : IFieldRepository
{
    private TicketDbContext _dbContext;
    public FieldRepository(TicketDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Field> GetAll()
    {
        var fields = _dbContext.Field.ToList();
        return fields;
    }

    public Field GetById(int id)
    {
        var field = _dbContext.Field.FirstOrDefault(x => x.Id == id);
        return field;
    }

    public void Add(Field field)
    {
        _dbContext.Field.Add(field);
    }

    public void Update(Field field)
    {
        _dbContext.Field.Update(field);
    }

    public void Remove(Field field)
    {
        _dbContext.Field.Remove(field);
    }
}
