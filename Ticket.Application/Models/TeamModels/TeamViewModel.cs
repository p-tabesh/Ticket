﻿namespace Ticket.Application.Models;

//public class TeamViewModel
//{
//    public int Id { get; set; }
//    public string TeamName { get; set; }
//    public int? UsersCount { get; set; }

//    public List<UserViewModel> Users { get; set; }
//}

public record TeamViewModel(int Id, string TeamName, int? UsersCount, List<UserViewModel> Users);
