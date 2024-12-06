using Microsoft.EntityFrameworkCore;
using Ticket.Infrastructure.Context;
using Ticket.Domain.IRepository;

namespace Ticket.Infrastructure.Repository;

public class GenericRepository<TEntity> where TEntity : class, IGenericRepositoy<TEntity>
{
    private TicketDbContext _context;
    private DbSet<TEntity> _dbset;

    public GenericRepository(TicketDbContext context)
    {
        _context = context;
        _dbset = _context.Set<TEntity>();
    }
    public void Add(TEntity entity)
    {
        _dbset.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbset.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbset.Remove(entity);
    }

    public TEntity GetById(int id)
    {
        TEntity entity = _dbset.Find(id);
        return entity;
    }

}
