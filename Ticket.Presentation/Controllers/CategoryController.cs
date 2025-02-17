using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Models;
using Ticket.Application.Models.CategoryModels;
using Ticket.Application.Services;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : Controller
{
    private CategoryService _categoryService;

    public CategoryController(CategoryService categoryService) => _categoryService = categoryService;


    [HttpGet]
    [Route("categories")]
    public IActionResult GetCategories([FromQuery] int? id)
    {
        var categories = _categoryService.GetCategories(id);
        return Json(categories);
    }


    [HttpPost]
    [Route("add-category")]
    public IActionResult AddCategory([FromBody] CategoryModel categoryModel)
    {
        _categoryService.AddCategory(categoryModel.Title, categoryModel.ParentCategory, categoryModel.UserId);
        return Json(new ResponseBaseModel());
    }

    [HttpPost]
    [Route("remove-category")]
    public IActionResult RemoveCategory(int categoryId)
    {
        _categoryService.RemoveCategory(categoryId);
        return Json(new ResponseBaseModel());
    }

    [HttpPut]
    [Route("edit-category-title")]
    public IActionResult EditCategoryTitle(int categoryId, string newTitle)
    {
        _categoryService.EditTitle(categoryId, newTitle);
        return Json(new ResponseBaseModel());
    }

    [HttpPut]
    [Route("update-defaultUserAssignee")]
    public IActionResult UpdateDefaultUserAssigne(int categoryId, int userId)
    {
        _categoryService.UpdateDefaultUserAssigne(categoryId, userId);
        return Json(new ResponseBaseModel());
    }

    [HttpPost]
    [Route("add-field")]
    public IActionResult AddFieldToCategory([FromBody] AddFieldModel addFieldModel)
    {
        _categoryService.AddField(addFieldModel.categoryId, addFieldModel.fieldId);
        return Json(new ResponseBaseModel());
    }

    [HttpGet]
    [Route("get-fields")]
    public IActionResult GetCategoryFields(int categoryId)
    {
        var categoryFields = _categoryService.GetCategoryFields(categoryId);
        return Ok(categoryFields);
    }
}