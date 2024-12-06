using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.Repository;

namespace Ticket.Infrastructure.UnitOfWork;

public class UnitOfWork
{
    private TicketRepository _ticketRepository;
    private UserRepository _userRepository;
    private CategoryRepository _categoryRepository;
    private TicketDbContext _context;

    public UnitOfWork(TicketDbContext context)
    {
        _context = context;
    }


    public TicketRepository TicketRepository
    {
        get
        {
            if (_ticketRepository == null)
                _ticketRepository = new TicketRepository(_context);
            return _ticketRepository;
        }
    }

    public UserRepository UserRepository
    {
        get
        {
            if (_userRepository == null)
                _userRepository = new UserRepository(_context);
            return _userRepository;
        }
    }

    public CategoryRepository CategoryRepository
    {
        get
        {
            if (CategoryRepository == null)
                _categoryRepository = new CategoryRepository(_context);
            return _categoryRepository;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}