using Ticket.Domain.Entity;

namespace Ticket.Application.Models;

public class TicketViewModel
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string ResponseBody { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string SubmitedUser { get; set; }
    public string AssignedUser { get; set; }
    public string Category { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public DateTime CreationDate { get; set; }
    public List<TicketAuditViewModel>? Audit { get; set;}
    public List<TicketNoteViewModel>? Notes { get; set; }
}
