using System.Collections.Generic;

namespace Ticket.Domain.Entity;

public class Category
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public Category? Parent { get; private set; }
    public int? ParentId { get; private set; }
    public User? DefaultUserAsign { get; private set; }
    public int? DefaultUserAsignId { get; private set; }
    public ICollection<Field>? Fields { get; private set; }
    public ICollection<Tickets>? Tickets { get; private set; }
    public Category()
    {
        Fields = new List<Field>();
    }

    public Category(string title, int? parentId, int? defaultUserAsignId)
    {
        if (string.IsNullOrEmpty(title))
            throw new Exception("Title cannot be empty");
        
        Title = title;
        ParentId = parentId;
        DefaultUserAsignId = defaultUserAsignId;
    }

    public void AddField(string fieldName, Enum.Type type, bool isRequired)
    {
        if (Fields == null)
            Fields = new List<Field>();

        var field = new Field(fieldName, type, isRequired);
        Fields.Add(field);
    }
}
