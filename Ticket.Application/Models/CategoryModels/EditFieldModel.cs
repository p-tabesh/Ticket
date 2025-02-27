using Ticket.Domain.Enums;

namespace Ticket.Application.Models;

public class EditFieldModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public FieldType? type { get; set; }
    public bool? IsRequired { get; set; }
}

