using Ticket.Domain.Enums;

namespace Ticket.Domain.Entity;

public class TicketStatusHistory
{
    public int Id { get; private set; }
    public Status Status { get; private set; }
    public Ticket Ticket { get; private set; }
    public int TicketId { get; private set; }

    private TicketStatusHistory() { }
    public TicketStatusHistory(Status status)
    {
        Status = status;
    }
}
