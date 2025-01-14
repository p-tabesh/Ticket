using Ticket.Application.Models;
using Ticket.Domain.Entity;
using Ticket.Domain.Enums;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.Context;
using System.Resources;
using System.Reflection;
using Ticket.Domain.IRepository;


namespace Ticket.Application.Services;

public class TicketService
{
    private ITicketRepository _ticketRepository;
    private ICategoryRepository _categoryRepository;
    private IUserRepository _userRepository;
    private TicketDbContext _ticketDbContext;
    public TicketService(TicketDbContext dbContext)
        => _ticketDbContext = dbContext;
    public void AddTicket(TicketModel ticketModel)
    {
        var resourceManager = new ResourceManager("Ticket.Application.Resources.CategoryExceptionMessages", Assembly.GetExecutingAssembly());

        using (var UoW = new UnitOfWork(_ticketDbContext))
        {

            var category = UoW.CategoryRepository.GetById(ticketModel.CategoryId);
            var user = UoW.UserRepository.GetById(ticketModel.UserId);
            var assignUser = UoW.CategoryRepository.GetDefaultUser(ticketModel.CategoryId);

            var ticket = new Tickets(ticketModel.Subject,
                                     ticketModel.Body,
                                     ticketModel.Priority,
                                     ticketModel.NationalCode,
                                     ticketModel.PhoneNumber,
                                     category,
                                     user);

            ticket.AssignTicket(_userRepository.GetById(2));
            ticket.AddStatusHistory(Status.Open);
            ticket.AddAudit(Domain.Enums.Action.Add, "added", user);
            UoW.TicketRepository.Add(ticket);
            UoW.Commit();
        }
    }
}



