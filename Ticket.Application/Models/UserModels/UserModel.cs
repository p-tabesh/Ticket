﻿namespace Ticket.Application.Models;

public class UserModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int TeamId { get; set; }
    public bool IsAdmin { get; set; }
}
