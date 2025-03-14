namespace Ticket.Application.Models;

public record UserViewModel(int Id, string Username, string Email, bool IsActive, bool IsAdmin, string Team);