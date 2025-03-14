using Ticket.Domain.Enums;

namespace Ticket.Application.Models;

public record EditFieldModel(int Id, string Name, FieldType? FieldType, bool IsRequired);
