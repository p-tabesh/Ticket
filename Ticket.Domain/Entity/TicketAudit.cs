namespace Ticket.Domain.Entity;

public class TicketAudit
{
    public int Id { get; private set; }
    public Enums.Action Action { get; private set; }
    public string Description { get; private set; }
    public int TicketId { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreationDate { get; private set; }
    public Ticket Ticket { get; private set; }
    public User User { get; private set; }

    private TicketAudit() { }

    public TicketAudit(Enums.Action action, string description, int userId)
    {
        Action = action;
        Description = description;
        UserId = userId;
        CreationDate = DateTime.UtcNow;
    }
}
