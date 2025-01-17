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

            ticket.AssignTicket(UoW.UserRepository.GetById(ticket.AssignUserId));
            ticket.AddStatusHistory(Status.Open);
            ticket.AddAudit(Domain.Enums.Action.Add, "added", user);
            UoW.TicketRepository.Add(ticket);
            UoW.Commit();
        }
    }

    public void AssignTicket(int ticketId, int userId)
    {
        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            var user = UoW.UserRepository.GetById(userId);
            var ticket = UoW.TicketRepository.GetById(ticketId);

            ticket.AssignTicket(user);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public void CloseTicket(int ticketId, string responseBody)
    {
        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            var ticket = UoW.TicketRepository.GetById(ticketId);
            ticket.CloseTicket(responseBody);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }

    public void AddNote(int ticketId, string note)
    {
        using (var UoW = new UnitOfWork(_ticketDbContext))
        {
            var ticket = UoW.TicketRepository.GetById(ticketId);
            ticket.AddNote(note);
            UoW.TicketRepository.Update(ticket);
            UoW.Commit();
        }
    }
}



