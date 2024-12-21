namespace Ticket.Domain.IRepository;

public interface IReadReposity<out TEntity>
{
    TEntity GetById(int id);
}
public interface IWriteRepository<in TEntity>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}
public interface IGenericRepositoy<TEntity>: IWriteRepository<TEntity>, IReadReposity<TEntity> 
    where TEntity:class 
{
    
}
