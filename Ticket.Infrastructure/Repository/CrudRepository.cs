using Microsoft.EntityFrameworkCore;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.Repository;

public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity: class
{
    private DbSet<TEntity> _dbSet;
    private TicketDbContext _dbContext;

    public CrudRepository(TicketDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }
    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public IEnumerable<TEntity> GetAll()
    {
        var entities = _dbSet.ToList();
        return entities;
    }

    public TEntity GetById(int id)
    {
        var entity = _dbSet.Find(id);
        return entity;
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
}
