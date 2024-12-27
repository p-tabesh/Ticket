using Ticket.Domain.Enums;

namespace Ticket.Application.Models;

public class TicketModel
{
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public Priority Priority { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
}

