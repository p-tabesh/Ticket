using Ticket.Domain.IRepository;
using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.Repository;

namespace Ticket.Infrastructure.UnitOfWork;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : TicketDbContext
{
    private readonly TContext _context;
    private readonly Dictionary<Type, object> _repositories;
    private bool _disposed;
    public UnitOfWork(TContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }


    public IGenericRepositoy<T> GetGenericRepositoy<T>() where T : class
    {
        if (_repositories.TryGetValue(typeof(T), out var repository))
        {
            return (IGenericRepositoy<T>)repository;
        }
        var newRepository = new GenericRepository<T>(_context);
        _repositories.Add(typeof(T), newRepository);
        return newRepository;
    }
    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _context.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    public void Rollback()
    {
        _context.ChangeTracker.Clear();
    }
}
