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

    public IEnumerable<CategoryViewModel> GetAllCategories()
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            List<Category> categoryList = new();

            var categories = UoW.CategoryRepository.GetAll();
            categoryList.AddRange(categories);

            var models = CategoryViewMapper.MapToDTO(categoryList);

            return models;
        }
    }

    public CategoryViewModel GetCategory(int id)
    {
        using (var UoW =new UnitOfWork(_dbContext))
        {
            var category = UoW.CategoryRepository.GetById(id);
            if (category == null)
                throw new BaseCustomException(ErrorType.NotFound,"Category doesn't exists");
            var model = CategoryViewMapper.MapToDTO(category);
            return model;
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
                throw new BaseCustomException(ErrorType.NotFound,"category doesnt exists");

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
                throw new BaseCustomException(ErrorType.NotFound, "category doesnt exists");

            if (string.IsNullOrEmpty(title))
                throw new BaseCustomException(ErrorType.ValidationError, "title must have value");



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

    public void AddField(int categoryId, int fieldId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var categoryField = UoW.CategoryFieldRepository.GetByCategoryIdAndFieldId(categoryId, fieldId);
            if (categoryField != null)
                throw new Exception("this field already exists for this category");

            var category = UoW.CategoryRepository.GetById(categoryId);
            if (category == null)
                throw new Exception("category doesnt exists");

            var field = UoW.FieldRepository.GetById(fieldId);
            if (field == null)
                throw new Exception("field doesnt exists");

            var newCategoryField = new CategoryField(category, field);
            UoW.CategoryFieldRepository.Add(newCategoryField);
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
    public CategoryFieldsViewModel GetCategoryFields(int categoryId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var category = UoW.CategoryRepository.GetById(categoryId);
            if (category == null)
                throw new Exception("category doesnt exists");

            var categoryFieldModel = CategoryFieldMapper.MapToDTO(category);
            return categoryFieldModel;
        }
    }
}
