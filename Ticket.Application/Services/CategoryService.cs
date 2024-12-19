using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Application.Models;
using System;

namespace Ticket.Application.Services;

public class CategoryService
{
    private ICategoryRepository _categoryRepository;
    private IUserRepository _userRepository;
    private ICategoryFieldRepository _categoryFieldRepository;
    private IGenericRepositoy<CategoryField> _categoryFieldGenericRepository;
    public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository, ICategoryFieldRepository categoryFieldRepository)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _categoryFieldRepository = categoryFieldRepository;
    }

    public void AddCategory(string title, int? parentId, int defaultUserAssingeId)
    {
        if (title == null)
            throw new ArgumentNullException("title must have value.");

        var user = _userRepository.GetById(defaultUserAssingeId);
        if (user == null)
            throw new ArgumentException("user doesnt exists");

        try
        {
            var category = new Category(title, parentId, user);
            _categoryRepository.Add(category);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public void UpdateDefaultUserAssigne(int categoryId, int userId)
    {
        var category = _categoryRepository.GetById(categoryId) ?? throw new Exception("category doesnt exists");
        var user = _userRepository.GetById(userId) ?? throw new Exception("user doesnt exists");

        if (category.DefaultUserAsign == user)
            throw new Exception("user already is default for this category");

        category.UpdateDefaultUserAssinge(user);
        _categoryRepository.Update(category);
    }

    public void AddField(FieldModel fieldModel)
    {
        var field = new Field(fieldModel.Name, fieldModel.FieldType, fieldModel.IsRequired);
        var category = _categoryRepository.GetById(fieldModel.CategoryId);
        var categoryField = new CategoryField(category, field);

        _categoryFieldRepository.Add(categoryField);
    }

}
