namespace Ticket.Domain.Entity;

public class CategoryField
{
    public int Id { get; private set; }
    public int CategoryId { get; private set; }
    public int FieldId { get; private set; }
    public Category Category { get; private set; }
    public Field Field { get; private set; }
    private CategoryField() { }
    public CategoryField(Category category, Field field)
    {
        if (category == null || field == null)
            throw new Exception("category or field doesnt exists");

        Category = category;
        Field = field;
    }
}
