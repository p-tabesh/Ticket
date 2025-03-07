using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public class TicketNoteMapper
{
    public static TicketNoteViewModel MapToDTO(TicketNote ticketNote)
    {
        var model = new TicketNoteViewModel
        {
            User = ticketNote.User.Username,
            Note = ticketNote.Note,
            CreationDate = ticketNote.CreationDate
        };

        return model;
    }
}
