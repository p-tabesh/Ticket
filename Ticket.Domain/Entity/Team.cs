namespace Ticket.Domain.Entity;

public class Team
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public ICollection<User>? Users { get; private set; }
    private Team() { }

    public Team(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new Exception("title invalid");

        Title = title;
    }
}
