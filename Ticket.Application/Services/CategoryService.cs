using Ticket.Domain.Entity;
using Ticket.Application.Models;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.Context;
using Ticket.Domain.Exceptions;

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


    public void RemoveCategory(int categoryId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            //try
            //{
                var category = UoW.CategoryRepository.GetById(categoryId);

                if (category == null)
                    throw new CategoryException("category doesnt exists");

                UoW.CategoryRepository.Delete(category);
                UoW.Commit();
            //}
            //catch (Exception ex)
            //{
            //    UoW.Rollback();
            //    throw new Exception(ex.Message, ex);
            //}
        }
    }

    public void EditCategoryTitle(int categoryId, string title)
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

    public void AddField(FieldModel fieldModel)
    {
        using var UoW = new UnitOfWork(_dbContext);
        var field = new Field(fieldModel.Name, fieldModel.FieldType, fieldModel.IsRequired);
        var category = UoW.CategoryRepository.GetById(fieldModel.CategoryId);
        var categoryField = new CategoryField(category, field);
        UoW.CategoryFieldRepository.Add(categoryField);
        UoW.Commit();
    }

    public void RemoveField(int categoryId, int fieldId)
    {
        using (var UoW = new UnitOfWork(_dbContext))
        {
            var field = UoW.CategoryFieldRepository.GetFields(categoryId).Where(f => f.FieldId == fieldId).FirstOrDefault();

            if (field == null)
                throw new Exception("field doesnt exists");

            UoW.CategoryFieldRepository.Remove(field);
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
            return new CategoryFieldsModel
            {
                CategoryId = category.Id,
                CategoryTitle = category.Title,
                Fields = category.GetFields()
            };
        }
    }
}
