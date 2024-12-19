using Ticket.Domain.Enums;

namespace Ticket.Domain.Entity;

public class Field
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public FieldType Type { get; private set; }
    public bool IsRequired { get; private set; }

    public ICollection<Category> Categories { get; private set; }
    public Field() { }
    public Field(string name, FieldType type, bool isRequired)
    {
        if (String.IsNullOrEmpty(name))
            throw new ArgumentNullException("name");
        Name = name;
        Type = type;
        IsRequired = isRequired;
    }
}
