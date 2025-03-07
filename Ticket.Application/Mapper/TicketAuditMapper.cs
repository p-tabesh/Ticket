using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public class TicketAuditMapper
{
    public static TicketAuditViewModel MapToDTO(TicketAudit? ticketAudit)
    {
        
        var model = new TicketAuditViewModel
        {
            Action = ticketAudit.Action.ToString(),
            Description = ticketAudit.Description,
            User = ticketAudit.User.Username,
            CreationDate = ticketAudit.CreationDate
        };
        return model;
    }
}
