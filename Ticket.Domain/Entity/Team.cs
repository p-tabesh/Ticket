namespace Ticket.Domain.Entity;

public class Team
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public ICollection<User>? Users { get; private set; }
    public Team()
    {
        
    }
}
