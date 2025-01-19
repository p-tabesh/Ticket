using Ticket.Domain.Entity;
using Ticket.Application.Models;
using Ticket.Domain.Enums;


namespace Ticket.Application.Mapper;

public class TicketMapper
{


    public TicketViewDTO MapToDTO(Tickets ticket)
    {
        var model = new TicketViewDTO()
        {
            Id = ticket.Id,
            Body = ticket.Body,
            CreationDate = ticket.CreationDate,
            Priority = ticket.Priority.ToString(),
            Status = ticket.Status.ToString(),
            Subject = ticket.Subject,
            SubmitedUser = ticket.User.Username,
            Category = ticket.Category.Title,
            AssignedUser = ticket.AssignUser.Username,
            NationalCode = ticket.NationalCode,
            PhoneNumber = ticket.PhoneNumber,
        };
        return model;
    }

    public Tickets MapToEntity(TicketDTO ticketModel)
    {
        var ticket = new Tickets(
            ticketModel.Subject,
            ticketModel.Body,
            ticketModel.Priority,
            ticketModel.NationalCode,
            ticketModel.PhoneNumber,
            ticketModel.CategoryId,
            ticketModel.SubmitedUserId);
        return ticket;
    }
}