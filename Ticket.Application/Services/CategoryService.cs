using Ticket.Domain.Entity;
using Ticket.Application.Models;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.Context;

namespace Ticket.Application.Services;

public class CategoryService
{
    TicketDbContext _dbContext;

    public CategoryService(TicketDbContext dbContext) => _dbContext = dbContext;

    public void AddCategory(string title, int? parentId, int defaultUserAssingeId)
    {
        using var UoW = new UnitOfWork(_dbContext);
        if (title == null)
            throw new ArgumentNullException("title must have value.");

        var user = UoW.UserRepository.GetById(defaultUserAssingeId);

        if (user == null)
            throw new ArgumentException("user doesnt exists");

        try
        {
            var category = new Category(title, parentId, user);
            UoW.CategoryRepository.Add(category);
        }
        catch (Exception e)
        {
            UoW.Rollback();
            throw new Exception(e.Message, e);
        }
    }

    public void UpdateDefaultUserAssigne(int categoryId, int userId)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var category = UoW.CategoryRepository.GetById(categoryId) ?? throw new Exception("category doesnt exists");
        var user = UoW.UserRepository.GetById(userId) ?? throw new Exception("user doesnt exists");

        if (category.DefaultUserAsign == user)
            throw new Exception("user already is default for this category");

        category.UpdateDefaultUserAssinge(user);
        UoW.CategoryRepository.Update(category);
        UoW.Commit();
    }

    public void AddField(FieldModel fieldModel)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var field = new Field(fieldModel.Name, fieldModel.FieldType, fieldModel.IsRequired);
        var category = UoW.CategoryRepository.GetById(fieldModel.CategoryId);
        var categoryField = new CategoryField(category, field);
        UoW.CategoryFieldRepository.Add(categoryField);
        UoW.Commit();
    }

    public IEnumerable<CategoryField> GetCategoryFields(int categoryId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var fields = UoW.CategoryFieldRepository.GetFields(categoryId);
            return fields;
        }
    }
}
