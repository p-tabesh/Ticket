using Ticket.Domain.Enum;

namespace Ticket.Application.Models;

public class TicketInfo
{
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public Priority Priority { get; set; }
}

