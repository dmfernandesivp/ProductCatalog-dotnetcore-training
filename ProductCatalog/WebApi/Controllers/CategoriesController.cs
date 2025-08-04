using Application.CategoryService;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
            return NotFound($"Category with ID {id} was not found");

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        // The global exception middleware will catch any exceptions from the service
        var createdCategory = await _categoryService.CreateCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
    {
        // The global exception middleware will catch ArgumentException from the service
        var updatedCategory = await _categoryService.UpdateCategoryAsync(id, category);
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        if (!result)
            return NotFound($"Category with ID {id} was not found");

        return NoContent();
    }
}
