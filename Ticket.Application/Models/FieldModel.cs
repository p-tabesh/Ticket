using Ticket.Domain.Enums;

namespace Ticket.Application.Models;

public class FieldModel
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public FieldType FieldType { get; set; }
    public bool IsRequired { get; set; }
}
