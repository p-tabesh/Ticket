using Ticket.Domain.Enum;

namespace Ticket.Domain.Entity;


public class Tickets
{
    public int Id { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public string ResponseBody { get; private set; }
    public Status Status { get; private set; }
    public Priority Priority { get; private set; }
    public string NationalCode { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime CreationDate { get; private set; }

    // Relations
    // Category
    public Category Category { get; private set; }
    public int CategoryId { get; private set; }

    // User
    public User User { get; private set; }
    public int UserId { get; private set; }
    // AssignUser
    public User AssignUser { get; private set; }
    public int AssignUserId { get; private set; }

    // Audit
    public ICollection<TicketAudit> TicketAudit {  get; private set; }
    public ICollection<TicketStatusHistory> TicketStatusHistory { get; private set; }
    public ICollection<TicketNote> TicketNote { get; private set; }
}
