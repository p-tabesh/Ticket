namespace Ticket.Domain.Entity;

public class TicketNote
{
    public int Id { get; private set; }
    public string Note { get; private set; }
    public int UserId { get; private set; }
    public int TicketId { get; private set; }
    public User User { get; private set; }
    public Tickets Ticket { get; private set; }
}
