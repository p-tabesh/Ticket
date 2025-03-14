using Ticket.Application.Models;
using Ticket.Domain.Entity;

namespace Ticket.Application.Mapper;

public static class FieldMapper
{
    public static FieldViewModel MapToDto(Field field)
    {
        var fieldModel = new FieldViewModel(field.Id, field.Name, field.Type.ToString(), field.IsRequired);
        
        return fieldModel;
    }
}
