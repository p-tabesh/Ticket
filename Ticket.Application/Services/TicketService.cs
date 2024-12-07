using Ticket.Domain.IRepository;
using Ticket.Application.Models;
using Ticket.Domain.Entity;
using Ticket.Domain.Enum;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.Context;
namespace Ticket.Application.Services;

public class TicketService
{
    //private ITicketRepository _ticketRepository;
    //private ICategoryRepository _categoryRepository;
    //private IUserRepository _userRepository;
    private TicketDbContext _context;
    public TicketService(TicketDbContext context)
    {
        //_ticketRepository = ticketRepository;
        //_categoryRepository = categoryRepository;
        //_userRepository = userRepository;
        _context = context;
    }


    public void AddTicket(TicketInfo ticketInfo, CustomerInfo customerInfo)
    {
        using (var UOW = new UnitOfWork(_context))
        {
            var user = UOW.UserRepository.GetById(ticketInfo.UserId);
            var assignUser = UOW.CategoryRepository.GetDefaultUser(ticketInfo.CategoryId);
            var category = UOW.CategoryRepository.GetById(ticketInfo.CategoryId);

            var ticket = new Tickets(ticketInfo.Subject,
                               ticketInfo.Body,
                               ticketInfo.Priority,
                               customerInfo.NationalCode,
                               customerInfo.PhoneNumber,
                               category,
                               assignUser,
                               user);

            ticket.AddStatusHistory(Status.Open);
            ticket.AddAudit(Domain.Enum.Action.Add, $"Ticket Added By {user.Username}", user);

            UOW.TicketRepository.Add(ticket);
            UOW.Save();
        }
    }
}
