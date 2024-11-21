using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entity;
using Ticket.Domain.IRepository;
using Ticket.Domain.Enums;
using System.Runtime.InteropServices.ComTypes;
using Ticket.Domain.Enum;

namespace Ticket.Application.Services;

public class CategoryService
{
    private ICategoryRepository _categoryRepository;
    private IUserRepository _userRepository;

    public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
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

    public void AddField(string fieldName, FieldType fieldType, bool isRequired)
    {

    }
    
}
