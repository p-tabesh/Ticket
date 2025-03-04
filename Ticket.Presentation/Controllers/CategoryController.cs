using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Models.CategoryModels;
using Ticket.Application.Services;
using Ticket.Presentation.Extentions;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : BaseController
{
    private CategoryService _categoryService;

    public CategoryController(CategoryService categoryService) => _categoryService = categoryService;


    [HttpGet]
    [Route("categories")]
    public IActionResult GetCategories([FromQuery] int? id)
    {
        var categories = _categoryService.GetCategories(id);
        return Ok(categories);
    }


    [HttpPost]
    [Route("add")]
    [Authorize(Policy = "Admin")]
    public IActionResult AddCategory([FromBody] AddCategoryModel categoryModel)
    {
        _categoryService.AddCategory(categoryModel.Title, categoryModel.ParentCategory, categoryModel.DefaultAssigneUserId);
        return Ok();
    }

    [HttpPost]
    [Route("remove")]
    [Authorize(Policy = "Admin")]
    public IActionResult RemoveCategory(int categoryId)
    {
        _categoryService.RemoveCategory(categoryId);
        return Ok();
    }

    [HttpPut]
    [Route("edit-title")]
    [Authorize(Policy = "Admin")]
    public IActionResult EditCategoryTitle(int categoryId, string newTitle)
    {
        _categoryService.EditTitle(categoryId, newTitle);
        return Ok();
    }

    [HttpPut]
    [Route("update-defaultUserAssignee")]
    [Authorize(Policy = "Admin")]
    public IActionResult UpdateDefaultUserAssigne(int categoryId, int userId)
    {
        _categoryService.UpdateDefaultUserAssigne(categoryId, userId);
        return Ok();
    }

    [HttpPost]
    [Route("add-field")]
    [Authorize(Policy = "Admin")]
    public IActionResult AddFieldToCategory([FromBody] AddFieldModel addFieldModel)
    {
        _categoryService.AddField(addFieldModel.categoryId, addFieldModel.fieldId);
        return Ok();
    }

    [HttpPost]
    [Route("remove-field")]
    [Authorize(Policy = "Admin")]
    public IActionResult RemoveFieldFromCategory(int categoryId, int fieldId)
    {
        _categoryService.RemoveField(categoryId, fieldId);
        return Ok();
    }

    [HttpGet]
    [Route("get-fields")]
    public IActionResult GetCategoryFields(int categoryId)
    {
        var categoryFields = _categoryService.GetCategoryFields(categoryId);
        return Ok(categoryFields);
    }
}