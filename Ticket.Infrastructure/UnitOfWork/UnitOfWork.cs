using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    public TicketDbContext Context;
    private bool _disposed;
    public UnitOfWork(TicketDbContext context)
    {
        Context = context;
    }

    public void Commit()
    {
        Context.SaveChanges();
    }
    public void Rollback()
    {
        Context.ChangeTracker.Clear();
    }
    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Context?.Dispose();
            }
            _disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }    
}


