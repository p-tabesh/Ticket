namespace Ticket.Domain.IRepository;

public interface ICrudRepository<TEntity>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    IEnumerable<TEntity> GetAll();
    TEntity GetById(int id);
}
