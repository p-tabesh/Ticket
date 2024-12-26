using Ticket.Application.Models;
using Ticket.Domain.Entity;
using Ticket.Domain.Enums;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.Context;
using System.Resources;
using System.Reflection;
using Ticket.Infrastructure.Repository;


namespace Ticket.Application.Services;

public class TicketService
{
    private TicketDbContext _ticketDbContext;
    public TicketService(TicketDbContext dbContext)
    {
        _ticketDbContext = dbContext;
    }
    public void AddTicket(TicketInfo ticketInfo, CustomerInfo customerInfo)
    {
        var resourceManager = new ResourceManager("Ticket.Application.Resources.CategoryExceptionMessages",Assembly.GetExecutingAssembly());

        using (var UoW = new UnitOfWork<TicketDbContext>(_ticketDbContext))
        {
            var ticketRepository = UoW.GetGenericRepositoy<Tickets>();
            var userRepository = UoW.GetGenericRepositoy<User>();
            var categoryRepository = UoW.GetGenericRepositoy<Category>();
            var category = categoryRepository.GetById(ticketInfo.CategoryId);
            var user = userRepository.GetById(ticketInfo.UserId);
            var assignUser = category.DefaultUserAsign;
            var ticket = new Tickets(ticketInfo.Subject,
                               ticketInfo.Body,
                               ticketInfo.Priority,
                               customerInfo.NationalCode,
                               customerInfo.PhoneNumber,
                               category,
                               assignUser,
                               user);
            ticket.AddStatusHistory(Status.Open);
            ticket.AddAudit(Domain.Enums.Action.Add, "added", user);
            ticketRepository.Add(ticket);
            UoW.Commit();
        }



    }
}
    


