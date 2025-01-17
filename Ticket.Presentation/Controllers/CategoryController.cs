using Microsoft.AspNetCore.Mvc;
using Ticket.Application.Services;
using Ticket.Application.Models;
using System.Text.Json;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : Controller
{
    private CategoryService _categoryService;

    public CategoryController(CategoryService categoryService) => _categoryService = categoryService;

    [HttpPost]
    [Route("add-category")]
    public IActionResult AddCategory([FromBody] CategoryModel categoryModel)
    {
        _categoryService.AddCategory(categoryModel.Title, categoryModel.ParentCategory, categoryModel.UserId);
        return Json(new ResponseBaseModel { IsSuccess = true, Message = "success", StatusCode = 200 });
    }

    [HttpPut]
    [Route("update-defaultUserAssignee")]
    public IActionResult UpdateDefaultUserAssigne(int categoryId, int userId)
    {
        _categoryService.UpdateDefaultUserAssigne(categoryId, userId);
        return Ok(new ResponseBaseModel { IsSuccess = true, Message = "success", StatusCode = 200 });
    }

    [HttpPost]
    [Route("add-field")]
    public IActionResult AddFieldToCategory([FromBody] FieldModel fieldModel)
    {
        _categoryService.AddField(fieldModel);
        return Ok(new ResponseBaseModel { IsSuccess = true, Message = "success", StatusCode = 200 });
    }

    [HttpGet]
    [Route("get-fields")]
    public IActionResult GetCategoryFields(int categoryId)
    {
        var fields = _categoryService.GetCategoryFields(categoryId);
        return Ok(fields);
    }
}