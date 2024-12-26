using Microsoft.EntityFrameworkCore;
using Ticket.Domain.IRepository;
using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.ContextStorage;

namespace Ticket.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork,ICallContextItem
{
    public DbContext TicketDbContext { get; }
    CallContextIdentifier ICallContextItem.ContextIdentifier { get; } = new();
    private IDbContextFactory<TicketDbContext> _dbContextFactory;

    private bool _disposed;
    private bool _completed;
    public UnitOfWork(IDbContextFactory<TicketDbContext> dbContextFactory, bool readOnly)
    {
        _dbContextFactory = dbContextFactory;
        TicketDbContext = dbContextFactory.CreateDbContext();
        if (!readOnly)
        {
            TicketDbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            TicketDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        CallContextStorage<UnitOfWork>.SetContext(this);
    }


    public void Commit()
    {
        if (_disposed)
        {
            throw new InvalidOperationException("Object disposed");
        }
        if (_completed)
        {
            throw new InvalidOperationException("this method should be called once");
        }
        TicketDbContext.SaveChanges();
        _completed = true;
    }


    public void Rollback()
    {
        TicketDbContext.ChangeTracker.Clear();
    }
    public void Dispose()
    {
        if (!_disposed)
        {
            TicketDbContext.Dispose();
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }

    public IGenericRepositoy<T> GetGenericRepositoy<T>() where T : class
    {
        throw new NotImplementedException();
    }
}