using Ticket.Infrastructure.Context;
using Ticket.Domain.IUnitOfWork;

namespace Ticket.Infrastructure.UnitsOfWork;

//public class UnitOfWork : IDisposable
//{

//    private IDbContextTransaction _transaction;
//    private TicketRepository _ticketRepository;
//    private UserRepository _userRepository;
//    private CategoryRepository _categoryRepository;
//    private GenericRepository<Category> _genericRepository;

//    private readonly TicketDbContext _context;

//    private readonly UnitOfWork? _parentUnitOfWork;

//    public UnitOfWork(TicketDbContext context)
//    {
//        _context = context;
//    }

//    public void BeginTransaction()
//    {
//        _transaction = _context.Database.BeginTransaction();
//    }

//    public TicketRepository TicketRepository
//    {
//        get
//        {
//            if (_ticketRepository == null)
//                _ticketRepository = new TicketRepository(_context);
//            return _ticketRepository;
//        }
//    }

//    public UserRepository UserRepository
//    {
//        get
//        {
//            if (_userRepository == null)
//                _userRepository = new UserRepository(_context);
//            return _userRepository;
//        }
//    }

//    public CategoryRepository CategoryRepository
//    {
//        get
//        {
//            if (_categoryRepository == null)
//                _categoryRepository = new CategoryRepository(_context);
//            return _categoryRepository;
//        }
//    }
//    public void Rollback()
//    {
//        _transaction?.Rollback();
//    }
//    public void Commit()
//    {
//        _context.SaveChanges();
//        _transaction?.Commit();
//    }


//    private bool _disposed = false;
//    protected virtual void Dispose(bool disposing)
//    {
//        if (!_disposed)
//        {
//            if (disposing)
//            {
//                _context.Dispose();
//            }
//        }
//        _context.Dispose();
//        _transaction?.Dispose();
//        _disposed = true;
//    }
//    public void Dispose()
//    {
//        Dispose(true);
//        GC.SuppressFinalize(this);
//    }
//}



//public class NastedUnitOfWork : UnitOfWork
//{
//    static private int _transactionDepth;

//    public NastedUnitOfWork(TicketDbContext context)
//        : base(context)
//    {
//        if (_transactionDepth == 0)
//        {
//            base.BeginTransaction();
//        }
//        _transactionDepth++;
//    }


//    public new void Commit()
//    {
//        _transactionDepth--;

//        if (_transactionDepth == 0)
//            base.Commit();
//    }

//    public new void Rollback()
//    {
//        _transactionDepth--;
//        if (_transactionDepth == 0)
//            base.Rollback();
//    }

//    public new void Dispose()
//    {
//        if (_transactionDepth == 0)
//            base.Dispose();
//    }
//}




public abstract class UnitOfWork : IUnitOfWork
{
    private readonly TicketDbContext _context;
    public UnitOfWork(TicketDbContext context)
    {
        _context = context;
    }
    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Rollback()
    {
        _context.ChangeTracker.Clear();
    }
}