namespace Ticket.Application.Models;

public class UserViewModel
{ 
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public bool IsAdmin { get; set; }
    public string Team {  get; set; }
}