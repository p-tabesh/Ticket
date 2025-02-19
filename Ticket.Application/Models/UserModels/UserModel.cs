namespace Ticket.Application.Models;

public class UserModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int TeamId { get; set; }
}

public class UserViewModel
{ 
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public string Team {  get; set; }
}