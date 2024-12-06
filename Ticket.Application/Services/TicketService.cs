using Ticket.Domain.IRepository;
using Ticket.Application.Models;
using Ticket.Domain.Entity;
using Ticket.Domain.Enum;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.Context;
namespace Ticket.Application.Services;

public class TicketService
{
    private ITicketRepository _ticketRepository;
    private ICategoryRepository _categoryRepository;
    private IUserRepository _userRepository;
    private TicketDbContext _context;
    public TicketService(ITicketRepository ticketRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, TicketDbContext context)
    {
        _ticketRepository = ticketRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _context = context;
    }


    public void AddTicket(TicketInfo ticketInfo, CustomerInfo customerInfo)
    {
        var user = _userRepository.GetById(ticketInfo.UserId);
        var assignUser = _categoryRepository.GetDefaultUser(ticketInfo.CategoryId);
        var category = _categoryRepository.GetById(ticketInfo.CategoryId);

        var ticket = new Tickets(ticketInfo.Subject,
                                 ticketInfo.Body,
                                 ticketInfo.Priority,
                                 customerInfo.NationalCode,
                                 customerInfo.PhoneNumber,
                                 category,
                                 assignUser,
                                 user);

        ticket.AddStatusHistory(Status.Open);

        ticket.AddAudit(Domain.Enum.Action.Add,$"Ticket Added By {user.Username}",user);
        
        //_ticketRepository.Add(ticket);

        using (var UOW = new UnitOfWork(_context))
        {
            UOW.TicketRepository.Add(ticket);
            UOW.Save();
        }
        

        
    }
}
