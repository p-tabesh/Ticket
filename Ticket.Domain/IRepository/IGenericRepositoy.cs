namespace Ticket.Domain.IRepository;

public interface IGenericRepositoy<TEntity>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    TEntity GetById(int id);
}
