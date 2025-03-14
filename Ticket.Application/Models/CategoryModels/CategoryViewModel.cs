namespace Ticket.Application.Models.CategoryModels;

public record CategoryViewModel(int CategoryId,
    string CategoryName,
    string? CategoryParentName,
    string? DefaultUserAssigneName);
