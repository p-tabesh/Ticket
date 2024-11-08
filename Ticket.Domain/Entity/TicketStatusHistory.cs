using Ticket.Domain.Enum;

namespace Ticket.Domain.Entity;

public class TicketStatusHistory
{
    public int Id { get; private set; }
    public Status Status { get; private set; }
}
