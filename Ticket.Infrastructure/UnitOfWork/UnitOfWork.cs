using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.Repository;

namespace Ticket.Infrastructure.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly TicketDbContext _context;
    private bool _disposed;

    private CategoryFieldRepository _categoryFieldRepository;
    private CategoryRepository _categoryRepository;
    private TeamRepository _teamRepository;
    private TicketRepository _ticketRepository;
    private UserRepository _userRepository;
    public UnitOfWork(TicketDbContext context)
    {
        _context = context;
    }

    public TicketRepository TicketRepository
    {
        get
        {
            if (_ticketRepository == null)
            {
                _ticketRepository = new TicketRepository(_context);
                return _ticketRepository;
            }
            return _ticketRepository;
        }
    }
    public UserRepository UserRepository
    {
        get
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository(_context);
            }
            return _userRepository;
        }
    }
    public TeamRepository TeamRepository
    {
        get
        {
            if (_teamRepository == null)
            {
                _teamRepository = new TeamRepository(_context);
                return _teamRepository;
            }
            return _teamRepository;
        }
    }

    public CategoryFieldRepository CategoryFieldRepository
    {
        get
        {
            if (_categoryFieldRepository == null)
            {
                _categoryFieldRepository = new CategoryFieldRepository(_context);
                return _categoryFieldRepository;
            }
            return _categoryFieldRepository;
        }
    }

    public CategoryRepository CategoryRepository
    {
        get
        {
            if (_categoryRepository == null)
            {
                _categoryRepository = new CategoryRepository(_context);
                return (_categoryRepository);
            }
            return _categoryRepository;
        }
    }
    public void Commit()
    {
        _context.SaveChanges();
    }
    public void Rollback()
    {
        _context.ChangeTracker.Clear();
    }
    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
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


