using Ticket.Domain.Enums;

namespace Ticket.Domain.Entity;


public class Tickets
{
    public int Id { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public string? ResponseBody { get; private set; }
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
    public User SubmitUser { get; private set; }
    public int SubmitUserId { get; private set; }
    // AssignUser
    public User AssignUser { get; private set; }
    public int AssignUserId { get; private set; }

    // Audit
    public ICollection<TicketAudit> TicketAudit { get; private set; }
    public ICollection<TicketStatusHistory> TicketStatusHistory { get; private set; }
    public ICollection<TicketNote> TicketNote { get; private set; }


    private Tickets() { }
    public Tickets(string subject,
                    string body,
                    Priority priority,
                    string nationalCode,
                    string phoneNumber,
                    int categoryId,
                    int submitUserId)
    {
        Subject = subject;
        Body = body;
        Priority = priority;
        NationalCode = nationalCode;
        PhoneNumber = phoneNumber;
        CategoryId = categoryId;
        SubmitUserId = submitUserId;
        CreationDate = DateTime.Now;
    }


    #region User
    public void AssignTicket(int userId)
    {

        this.AssignUserId = userId;
        AddAudit(Enums.Action.Update, $"Ticket Assigned to userid: {userId}", userId);
    }
    #endregion

    #region TicketStatusHistory
    public void AddStatusHistory(Status status, int userId)
    {
        if (TicketStatusHistory == null)
            TicketStatusHistory = new List<TicketStatusHistory>();

        var ticketStatusHistory = new TicketStatusHistory(status);
        TicketStatusHistory.Add(ticketStatusHistory);

        AddAudit(Enums.Action.Update, $"Ticket status change to {status}",userId);
    }
    #endregion  


    #region TicketAudit
    public void AddAudit(Enums.Action action, string description, int userId)
    {
        if (TicketAudit == null)
            TicketAudit = new List<TicketAudit>();

        var ticketAudit = new TicketAudit(action, description, userId);
        TicketAudit.Add(ticketAudit);
    }
    #endregion

    public void CloseTicket(string responseBody)
    {
        if (string.IsNullOrEmpty(responseBody))
            throw new InvalidOperationException();
        this.ResponseBody = responseBody;
        AddStatusHistory(Status.Closed);
        AddAudit(Enums.Action.Update, $"ticket closed with: {responseBody}", this.SubmitUserId);
    }

    public void AddNote(string note)
    {
        if (string.IsNullOrEmpty(note))
            throw new InvalidOperationException();
        if (TicketNote == null)
            TicketNote = new List<TicketNote>();
        var ticketNote = new TicketNote(note);
        TicketNote.Add(ticketNote);
    }
}
