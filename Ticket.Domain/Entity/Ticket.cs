using System.ComponentModel.DataAnnotations.Schema;
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
    public User User { get; private set; }
    public int UserId { get; private set; }
    // AssignUser
    public User AssignUser { get; private set; }
    public int AssignUserId { get; private set; }

    // Audit
    public ICollection<TicketAudit> TicketAudit { get; private set; }
    public ICollection<TicketStatusHistory> TicketStatusHistory { get; private set; }
    public ICollection<TicketNote> TicketNote { get; private set; }

    /// <summary>
    /// this constructor for ef core
    /// </summary>
    private Tickets()
    {
        //TicketAudit = new List<TicketAudit>();
        //TicketStatusHistory = new List<TicketStatusHistory>();
    }
    public Tickets(string subject,
                    string body,
                    Priority priority,
                    string nationalCode,
                    string phoneNumber,
                    Category category,
                    User user)
    {
        Subject = subject;
        Body = body;
        Priority = priority;
        NationalCode = nationalCode;
        PhoneNumber = phoneNumber;
        Category = category;
        User = user;
        CreationDate = DateTime.Now;
    }


    #region User
    public void AssignTicket(User user)
    {
        if (user is null)
        {
            throw new Exception("user doesnt exists");
        } 
        if (!user.IsActive)
        {
            throw new Exception("cannot assign ticket to inactive user");
        }
        if (user == this.User)
        {
            throw new InvalidOperationException("invalid operation");
        }
        this.AssignUser = user;
        AddAudit(Enums.Action.Update, $"Ticket Assigned to {user.Username}", user);
    }
    #endregion

    #region TicketStatusHistory
    public void AddStatusHistory(Status status)
    {
        if (TicketStatusHistory == null)
            TicketStatusHistory = new List<TicketStatusHistory>();

        var ticketStatusHistory = new TicketStatusHistory(status);
        TicketStatusHistory.Add(ticketStatusHistory);
    }
    #endregion  


    #region TicketAudit
    public void AddAudit(Enums.Action action, string description, User user)
    {
        if (TicketAudit == null)
            TicketAudit = new List<TicketAudit>();

        var ticketAudit = new TicketAudit(action, description, user);
        TicketAudit.Add(ticketAudit);
    }
    #endregion

    public void CloseTicket(string responseBody)
    {
        if (string.IsNullOrEmpty(responseBody))
            throw new InvalidOperationException();
        this.ResponseBody = responseBody;
        AddStatusHistory(Status.Closed);
        AddAudit(Enums.Action.Update, $"ticket closed with: {responseBody}", this.User);
    }
    
    public void AddNote(string note)
    {
        if (string.IsNullOrEmpty (note))
            throw new InvalidOperationException();
        if (TicketNote == null)
            TicketNote = new List<TicketNote>();
        var ticketNote = new TicketNote(note);
        TicketNote.Add(ticketNote);
    }
}
