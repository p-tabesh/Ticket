namespace Ticket.Domain.Entity;

public class User
{
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreationDate { get; private set; }
    public Team Team { get; private set; }
    public int TeamId { get; private set; }

    public ICollection<Tickets> Tickets { get; private set; }
    public ICollection<Tickets> AssignedTickets { get; private set; }
    public ICollection<TicketAudit> TicketAudits { get; private set; }
    public ICollection<TicketNote> Notes { get; private set; }
    public ICollection<Category> Categories { get; private set; }

    private User() { }
    public User(string username, string password, string email, int teamId)
    {
        Username = username.ToLower();
        Password = password;
        Email = email;
        TeamId = teamId;
        IsActive = true;
        CreationDate = DateTime.Now;
    }


    public void ChangePassword(string newPassword)
    {
        if (string.IsNullOrEmpty(newPassword))
            throw new Exception("password invalid");

        Password = newPassword;
    }

    public void ChangeUsername(string newUsername)
    {
        if (string.IsNullOrEmpty(newUsername))
            throw new Exception("username invalid");

        // and check if username doesnt exists 

        Username = newUsername.ToLower();
    }

    public void DeActive()
    {
        if (IsActive == false)
            throw new Exception("user already inActive");

        IsActive = false;
    }

    public void Active()
    {
        if (IsActive == true)
            throw new Exception("user already active");

        IsActive = true;
    }

    public void ChangeTeam(int teamId)
    {
        var teams = new List<Team>();
        if (!teams.Any(t => t.Id == teamId))
            throw new Exception("team doesnt exists");

        TeamId = teamId;
    }
}
