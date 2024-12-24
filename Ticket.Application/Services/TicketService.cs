using Ticket.Application.Models;
using Ticket.Domain.Entity;
using Ticket.Domain.Enums;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.Context;
using System.Resources;
using System.Reflection;

namespace Ticket.Application.Services;

public class TicketService
{
    //private ITicketRepository _ticketRepository;
    //private ICategoryRepository _categoryRepository;
    //private IUserRepository _userRepository;
    private TicketDbContext _context;
    public TicketService(TicketDbContext context)
    {
        _context = context;
    }
    public void AddTicket(TicketInfo ticketInfo, CustomerInfo customerInfo)
    {
        var resourceManager = new ResourceManager("Ticket.Application.Resources.CategoryExceptionMessages",Assembly.GetExecutingAssembly());

        using (var mainUoW = new NastedUnitOfWork(_context))
        {
            var category = mainUoW.CategoryRepository.GetById(ticketInfo.CategoryId);
            if (category == null)
                //throw new CategoryException(resourceManager.GetString("CategoryNotFound"));
                throw new InvalidOperationException();
            
            var assignUser = category.DefaultUserAsign;
            var user = mainUoW.UserRepository.GetById(ticketInfo.UserId);

            

            var ticket = new Tickets(ticketInfo.Subject,
                               ticketInfo.Body,
                               ticketInfo.Priority,
                               customerInfo.NationalCode,
                               customerInfo.PhoneNumber,
                               category,
                               assignUser,
                               user);

            ticket.AddStatusHistory(Status.Open);
            ticket.AddAudit(Domain.Enums.Action.Add, $"Ticket Added By {user.Username}", user);

            
            mainUoW.TicketRepository.Add(ticket);
            mainUoW.Commit();
        }

        
    }
}
