using Ticket.Domain.Enum;

namespace Ticket.Domain.Entity;

public class TicketAudit
{
    public int Id { get; private set; }
    public Enum.Action Action { get; private set; }
    public string Description { get; private set; }
    public DateTime CreationDate { get; private set; }

    // Ticket
    public Ticket Ticket { get; private set; }
    public int TicketId { get; private set; }

    //User
    public User User { get; private set; }
    public int UserId { get; private set; }

}
