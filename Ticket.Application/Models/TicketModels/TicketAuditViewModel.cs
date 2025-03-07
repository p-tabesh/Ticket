namespace Ticket.Application.Models;

public class TicketAuditViewModel
{
    public string User { get; set; }
    public string Action { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
}
