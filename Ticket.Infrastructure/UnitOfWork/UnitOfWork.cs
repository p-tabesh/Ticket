using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Ticket.Domain.IRepository;
using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.Repository;

namespace Ticket.Infrastructure.UnitOfWork;

public sealed class UnitOfWork: IUnitOfWork
{
    private TicketDbContext _dbContext;
    private bool _disposed;
    public UnitOfWork(ContextFactory dbContextFactory)
    {
        _dbContext = dbContextFactory.Context;
    }
    
    public void Commit()
    {
        _dbContext.Commit();
    }
    
    public void Dispose()
    {
        if (!_disposed)
        {
            _dbContext.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    public void Rollback()
    {
        _dbContext.Rollback();
    }
}


public class ContextFactory
{
    private TicketDbContext _dbContext;
    private IDbContextFactory<TicketDbContext> _dbContextFactory;

    public ContextFactory(IDbContextFactory<TicketDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }
    public TicketDbContext Context
    {
        get
        {
            if (_dbContext == null)
                return _dbContextFactory.CreateDbContext();
            return _dbContext;
        }
    }
}