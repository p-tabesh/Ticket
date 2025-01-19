using Ticket.Domain.Entity;
using Ticket.Domain.Enums;

namespace Ticket.Application.Models;

public class TicketDTO
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public int SubmitedUserId { get; set; }
    public int CategoryId { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
}

public class TicketViewDTO
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string SubmitedUser { get; set; }
    public string AssignedUser { get; set; }
    public string Category {  get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public DateTime CreationDate { get; set; }
}

