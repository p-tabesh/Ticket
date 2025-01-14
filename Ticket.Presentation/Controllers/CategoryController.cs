using Microsoft.AspNetCore.Mvc;
using Ticket.Domain.Entity;
using Ticket.Application.Services;
using Ticket.Application.Models;
using Prometheus;

namespace Ticket.Presentation.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        //_counter = Metrics.CreateCounter("TestCounterMetric", "test metric counter");
        //_gauge = Metrics.CreateGauge("GojeTest", "GOJE GILASI");
        _categoryService = categoryService;
    }
    [HttpPost]
    [Route("add-category")]
    public IActionResult AddCategory([FromQuery] CategoryModel categoryModel)
    {
        try
        {
            //_counter.Inc(1);
            //_counter.Publish();
            _categoryService.AddCategory(categoryModel.Title,categoryModel.ParentCategory,categoryModel.UserId);
            return Ok("Category Added Successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
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

    [HttpPost]
    [Route("add-field")]
    public IActionResult AddFieldToCategory(FieldModel fieldModel)
    {
        try
        {
            _categoryService.AddField(fieldModel);
            return Ok("Field added to category successfuly");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}

