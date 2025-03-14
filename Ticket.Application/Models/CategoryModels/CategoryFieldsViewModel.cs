namespace Ticket.Application.Models;

public record CategoryFieldsViewModel(int CategoryId, string CategoryTitle, List<FieldViewModel> Fields);

