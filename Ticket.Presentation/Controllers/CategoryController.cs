using Microsoft.AspNetCore.Mvc;
using Ticket.Domain.Entity;
using Ticket.Application.Services;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private CategoryService _categoryService;
    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    [Route("add-category")]
    public IActionResult AddCategory(string title, int? parentCategoryId, int userId)
    {
        try
        {
            _categoryService.AddCategory(title, parentCategoryId, userId);
            return Ok("Category Added Successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("update-defaultUser")]
    public IActionResult UpdateDefaultUserAssigne(int categoryId, int userId)
    {
        try
        {
            _categoryService.UpdateDefaultUserAssigne(categoryId, userId);
            return Ok("default user updated");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

