using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class FieldMapper
{
    public static FieldModel MapToDto(Field field)
    {
        var fieldModel = new FieldModel()
        {
            FieldType = field.Type,
            IsRequired = field.IsRequired,
            Name = field.Name,
        };
        return fieldModel;
    }
}
