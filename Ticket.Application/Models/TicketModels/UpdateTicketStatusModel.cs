using Ticket.Domain.Enums;

namespace Ticket.Application.Models.TicketModels;

public class UpdateTicketStatusModel
{
    public int TicketId { get; set; }
    public Status Status { get; set; }
}
