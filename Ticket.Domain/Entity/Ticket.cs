using Ticket.Domain.Enums;

namespace Ticket.Domain.Entity;


public class Ticket
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
    public int AssignUserId { get; private set; }
    public int CategoryId { get; private set; }
    public int SubmitUserId { get; private set; }
    public Category Category { get; private set; }
    public User SubmitUser { get; private set; }
    public User AssignUser { get; private set; }

    public ICollection<TicketAudit> TicketAudit { get; private set; }
    public ICollection<TicketStatusHistory> TicketStatusHistory { get; private set; }
    public ICollection<TicketNote> TicketNote { get; private set; }

    private Ticket() { }

    public Ticket(string subject,
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
    public void AssignTicket(User assignedUser, int assignerUserId)
    {

        this.AssignUser = assignedUser;
        AddAudit(Enums.Action.Update, $"Ticket Assigned to user: {assignedUser.Username}", assignerUserId);
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

    #region TicketChanges
    public void FinishTicket(string responseBody, int userId)
    {
        if (string.IsNullOrEmpty(responseBody))
            throw new InvalidOperationException();
        this.ResponseBody = responseBody;
        this.Status = Status.Finished;
        AddStatusHistory(Status.Finished, userId);
        AddAudit(Enums.Action.Update, $"Ticket finished", this.SubmitUserId);
    }

    public void CloseTicket()
    {
        Status = Status.Closed;
        AddStatusHistory(Status.Closed, 1);
        AddAudit(Enums.Action.StatusChange, "Ticket Closed", 1);
    }
    

    public void ChangeStatus(Status newStatus)
    {
        if (Status == newStatus)
            throw new Exception($"ticket already {newStatus}");
        Status = newStatus;
    }
    #endregion
    public void AddNote(string note, int userId)
    {
        if (string.IsNullOrEmpty(note))
            throw new InvalidOperationException();
        if (TicketNote == null)
            TicketNote = new List<TicketNote>();
        var ticketNote = new TicketNote(note, userId);
        TicketNote.Add(ticketNote);
    }
}
