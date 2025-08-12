using Application.TodoApp;
using Domain.TodoApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
    {
        var todos = await _todoService.GetAllTodosAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodo(Guid id)
    {
        var todo = await _todoService.GetTodoByIdAsync(id);
        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpGet("completed")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetCompletedTodos()
    {
        var todos = await _todoService.GetCompletedTodosAsync();
        return Ok(todos);
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetPendingTodos()
    {
        var todos = await _todoService.GetPendingTodosAsync();
        return Ok(todos);
    }

    [HttpGet("priority/{priority}")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodosByPriority(string priority)
    {
        var todos = await _todoService.GetTodosByPriorityAsync(priority);
        return Ok(todos);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodosByCategory(string category)
    {
        var todos = await _todoService.GetTodosByCategoryAsync(category);
        return Ok(todos);
    }

    [HttpGet("due-today")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodosDueToday()
    {
        var todos = await _todoService.GetTodosDueTodayAsync();
        return Ok(todos);
    }

    [HttpGet("overdue")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetOverdueTodos()
    {
        var todos = await _todoService.GetOverdueTodosAsync();
        return Ok(todos);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> SearchTodos([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return BadRequest("Search term is required.");

        var todos = await _todoService.SearchTodosAsync(term);
        return Ok(todos);
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IEnumerable<string>>> GetCategories()
    {
        var categories = await _todoService.GetCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("stats")]
    public async Task<ActionResult> GetTodoStats()
    {
        var stats = await _todoService.GetTodoStatsAsync();
        return Ok(stats);
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem todoItem)
    {
        try
        {
            todoItem.Id = Guid.Empty;
            var createdTodo = await _todoService.CreateTodoAsync(todoItem);
            return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id }, createdTodo);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TodoItem>> UpdateTodo(Guid id, TodoItem todoItem)
    {
        if (id != todoItem.Id && todoItem.Id != Guid.Empty)
            return BadRequest("ID mismatch between route and body.");

        todoItem.Id = id;

        try
        {
            var updatedTodo = await _todoService.UpdateTodoAsync(todoItem);
            return Ok(updatedTodo);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/toggle")]
    public async Task<IActionResult> ToggleCompletion(Guid id)
    {
        try
        {
            var success = await _todoService.ToggleTodoCompletionAsync(id);
            if (success)
                return NoContent();
            return NotFound();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        try
        {
            await _todoService.DeleteTodoAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
