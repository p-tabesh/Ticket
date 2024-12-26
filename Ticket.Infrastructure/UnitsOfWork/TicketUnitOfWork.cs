using Ticket.Domain.IRepository;
using Ticket.Infrastructure.Context;

namespace Ticket.Infrastructure.UnitsOfWork;

public class TicketUnitOfWork : UnitOfWork
{
    private readonly TicketDbContext _context;
    public ITicketRepository ticketRepository;
    public ICategoryRepository categoryRepository;
    public IUserRepository userRepository;
    public TicketUnitOfWork(TicketDbContext context, ITicketRepository ticketRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        : base(context)
    {
        _context = context;
        this.ticketRepository = ticketRepository;
        this.categoryRepository = categoryRepository;
        this.userRepository = userRepository;   
    }
    
    
    
}
