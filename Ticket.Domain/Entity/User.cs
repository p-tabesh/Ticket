using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Domain.Entity;

public class User
{
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public Team Team { get; private set; }
    public int TeamId { get; private set; }

    public ICollection<Tickets> Tickets {  get; private set; }
    public ICollection<Tickets> AssignedTickets { get; private set; }
    public ICollection<TicketAudit> TicketAudits { get; private set; }
    public ICollection<TicketNote> Notes { get; private set; }
    public ICollection<Category> Categories { get; private set; }

    public User()
    {
        
    }
    public User(string username, string password, string email, Team team)
    {
        Username = username;
        Password = password;
        Email = email;
        Team = team;
    }
}
