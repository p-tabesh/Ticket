using Ticket.Domain.Entity;

namespace Ticket.Domain.IRepository;

public interface ICategoryFieldRepository
{

    IEnumerable<CategoryField> GetByCategoryId(int categoryId);
    IEnumerable<CategoryField> GetByFieldId(int fieldId);
    CategoryField GetByCategoryIdAndFieldId(int categoryId, int fieldId);
    void Add(CategoryField categoryField);
    void Remove(CategoryField categoryField);

    IEnumerable<CategoryField> GetAll();

}
