using Ticket.Application.Mapper;
using Ticket.Application.Models;
using Ticket.Application.Models.CategoryModels;
using Ticket.Domain.Entity;
using Ticket.Domain.Exceptions;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;

namespace Ticket.Application.Services;

public class CategoryService
{
    TicketDbContext _dbContext;

    public CategoryService(TicketDbContext dbContext) => _dbContext = dbContext;

    public IEnumerable<CategoryViewModel> GetCategories()
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var categoryModels = new List<CategoryViewModel>();
            var categories = UoW.CategoryRepository.GetAll();
            foreach (var category in categories)
            {
                var model = CategoryViewMapper.MapToDTO(category);
                categoryModels.Add(model);
            }
            return categoryModels;
        }
    }
    public void AddCategory(string title, int? parentId, int defaultUserAssingeId)
    {
        using var UoW = new UnitOfWork(_dbContext);

        if (title == null)
            throw new ArgumentNullException("title must have value.");

        var user = UoW.UserRepository.GetById(defaultUserAssingeId);

        if (user == null)
            throw new ArgumentException("user doesnt exists");

        var category = new Category(title, parentId, user);
        UoW.CategoryRepository.Add(category);
        UoW.Commit();
    }


    public void RemoveCategory(int categoryId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var category = UoW.CategoryRepository.GetById(categoryId);

            if (category == null)
                throw new CategoryException("category doesnt exists");

            UoW.CategoryRepository.Delete(category);
            UoW.Commit();
        }
    }

    public void EditTitle(int categoryId, string title)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var category = UoW.CategoryRepository.GetById(categoryId);

            if (category == null)
                throw new CategoryException("category doesnt exists");

            if (string.IsNullOrEmpty(title))
                throw new CategoryException("title must have value");


            category.EditTitle(title);
            UoW.CategoryRepository.Update(category);
            UoW.Commit();
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

    public void AddField(int categoryId, FieldModel fieldModel)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var field = new Field(fieldModel.Name, fieldModel.FieldType, fieldModel.IsRequired);
            var category = UoW.CategoryRepository.GetById(categoryId);
            var categoryField = new CategoryField(category, field);
            UoW.CategoryFieldRepository.Add(categoryField);
            UoW.Commit();
        }
    }

    public void RemoveField(int categoryId, int fieldId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {

            var category = UoW.CategoryRepository.GetById(categoryId);
            category.RemoveField(fieldId);
            UoW.CategoryRepository.Update(category);
            UoW.Commit();
        }
    }

    public void EditField(EditFieldModel model)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var field = UoW.FieldRepository.GetById(model.Id);
            field.Edit(model.Name, model.type, model.IsRequired);
            UoW.FieldRepository.Update(field);
        }
    }
    public CategoryFieldsModel GetCategoryFields(int categoryId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var category = UoW.CategoryRepository.GetById(categoryId);
            var categoryFieldModel = CategoryFieldMapper.MapToDTO(category);
            return categoryFieldModel;
        }
    }
}
