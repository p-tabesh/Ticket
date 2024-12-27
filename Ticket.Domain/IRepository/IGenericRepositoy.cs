using System.Linq.Expressions;

namespace Ticket.Domain.IRepository;

public interface IReadReposity<TEntity>
{
    IEnumerable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression);
    TEntity GetById(int id);
    IQueryable<TEntity> GetAll();
}
public interface IWriteRepository<TEntity>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}
public interface IGenericRepositoy<TEntity> : IWriteRepository<TEntity>, IReadReposity<TEntity>
    where TEntity : class
{

}
