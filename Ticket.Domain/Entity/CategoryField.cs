namespace Ticket.Domain.Entity;

public class CategoryField
{
    public int Id { get; private set; }
    public Category Category { get; private set; }
    public int CategoryId { get; private set; }
    public Field Field { get; private set; }
    public int FieldId { get; private set; }
    private CategoryField() { }
    public CategoryField(Category category, Field field)
    {
        if (category == null || field == null)
            throw new Exception("category or field doesnt exists");

        var categoryFields = new List<CategoryField>();
        var categoryField = categoryFields.FirstOrDefault(cf => cf.FieldId == field.Id && cf.CategoryId == category.Id);
        if (categoryField != null)
            throw new Exception("already exists");

        Category = category;
        Field = field;
    }
}
