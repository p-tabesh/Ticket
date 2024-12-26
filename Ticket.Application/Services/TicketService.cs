using Ticket.Application.Models;
using Ticket.Domain.Entity;
using Ticket.Domain.Enums;
using Ticket.Infrastructure.UnitsOfWork;
using Ticket.Infrastructure.Context;
using System.Resources;
using System.Reflection;
using Ticket.Infrastructure.Repository;


namespace Ticket.Application.Services;

public class TicketService
{
    private TicketUnitOfWork _unitOfWork;
    public TicketService(TicketUnitOfWork ticketUnitOfWork)
    {
        _unitOfWork = ticketUnitOfWork;
    }
    public void AddTicket(TicketInfo ticketInfo, CustomerInfo customerInfo)
    {
        var resourceManager = new ResourceManager("Ticket.Application.Resources.CategoryExceptionMessages",Assembly.GetExecutingAssembly());

        var category = _unitOfWork.categoryRepository.GetById(ticketInfo.CategoryId);
        var assignUser = _unitOfWork.categoryRepository.GetDefaultUser(ticketInfo.CategoryId);
        var user = _unitOfWork.userRepository.GetById(ticketInfo.UserId);

        var ticket = new Tickets(ticketInfo.Subject,
                           ticketInfo.Body,
                           ticketInfo.Priority,
                           customerInfo.NationalCode,
                           customerInfo.PhoneNumber,
                           category,
                           assignUser,
                           user);

        _unitOfWork.ticketRepository.Add(ticket);
        _unitOfWork.Commit();
    }
}
    


