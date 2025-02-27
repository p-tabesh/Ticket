using Ticket.Domain.Enums;

namespace Ticket.Application.Models;

public class TicketFilterDTO
{
    public Status? Status { get; set; }
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
    public int? UserId { get; set; }
    public int? CategoryId { get; set; }
    public Priority? Priority { get; set; }
}
