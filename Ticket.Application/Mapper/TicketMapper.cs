using Ticket.Application.Models;
using Ticket.Domain.Entity;


namespace Ticket.Application.Mapper;

public static class TicketMapper
{

    public static TicketViewModel MapToDTO(Tickets ticket)
    {
        var model = new TicketViewModel()
        {
            Id = ticket.Id,
            Body = ticket.Body,
            CreationDate = ticket.CreationDate,
            Priority = ticket.Priority.ToString(),
            Status = ticket.Status.ToString(),
            Subject = ticket.Subject,
            SubmitedUser = ticket.SubmitUser.Username,
            Category = ticket.Category.Title,
            AssignedUser = ticket.AssignUser.Username,
            NationalCode = ticket.NationalCode,
            PhoneNumber = ticket.PhoneNumber,
        };
        return model;
    }

    public static Tickets MapToEntity(AddTicketModel ticketModel)
    {
        var ticket = new Tickets(
            ticketModel.Subject,
            ticketModel.Body,
            ticketModel.Priority,
            ticketModel.NationalCode,
            ticketModel.PhoneNumber,
            ticketModel.CategoryId,
            2);
        return ticket;
    }
}