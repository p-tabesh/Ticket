using Ticket.Domain.IRepository;
using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.Repository;

namespace Ticket.Infrastructure.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly TicketDbContext _dbContext;
    private bool _disposed;

    private ICategoryFieldRepository _categoryFieldRepository;
    private ICategoryRepository _categoryRepository;
    private ITeamRepository _teamRepository;
    private ITicketRepository _ticketRepository;
    private IUserRepository _userRepository;
    private IFieldRepository _fieldRepository;

    public UnitOfWork(TicketDbContext context)
    {
        _dbContext = context;
    }

    public ITicketRepository TicketRepository => _ticketRepository ??= new TicketRepository(_dbContext);
    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_dbContext);
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);
    public ITeamRepository TeamRepository => _teamRepository ??= new TeamRepository(_dbContext);
    public ICategoryFieldRepository CategoryFieldRepository => _categoryFieldRepository ??= new CategoryFieldRepository(_dbContext);
    public IFieldRepository FieldRepository => _fieldRepository ??= new FieldRepository(_dbContext);

    public void Commit()
    {
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            Rollback();
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public void Rollback()
    {
        _dbContext.ChangeTracker.Clear();
    }

    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext?.Dispose();
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


