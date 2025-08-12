using Domain.TodoApp;
using Persistence.TodoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoApp;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoItem?> GetTodoByIdAsync(Guid id)
    {
        return await _todoRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
    {
        return await _todoRepository.GetAllAsync();
    }

    public async Task<IEnumerable<TodoItem>> GetCompletedTodosAsync()
    {
        return await _todoRepository.GetCompletedAsync(true);
    }

    public async Task<IEnumerable<TodoItem>> GetPendingTodosAsync()
    {
        return await _todoRepository.GetCompletedAsync(false);
    }

    public async Task<IEnumerable<TodoItem>> GetTodosByPriorityAsync(string priority)
    {
        return await _todoRepository.GetByPriorityAsync(priority);
    }

    public async Task<IEnumerable<TodoItem>> GetTodosByCategoryAsync(string category)
    {
        return await _todoRepository.GetByCategoryAsync(category);
    }

    public async Task<IEnumerable<TodoItem>> GetTodosDueTodayAsync()
    {
        return await _todoRepository.GetDueTodayAsync();
    }

    public async Task<IEnumerable<TodoItem>> GetOverdueTodosAsync()
    {
        return await _todoRepository.GetOverdueAsync();
    }

    public async Task<IEnumerable<TodoItem>> SearchTodosAsync(string searchTerm)
    {
        return await _todoRepository.SearchAsync(searchTerm);
    }

    public async Task<TodoItem> CreateTodoAsync(TodoItem todoItem)
    {
        todoItem.Id = Guid.NewGuid();
        todoItem.CreatedAt = DateTime.UtcNow;
        todoItem.UpdatedAt = DateTime.UtcNow;

        return await _todoRepository.CreateAsync(todoItem);
    }

    public async Task<TodoItem> UpdateTodoAsync(TodoItem todoItem)
    {
        var existingTodo = await _todoRepository.GetByIdAsync(todoItem.Id);
        if (existingTodo == null)
            throw new KeyNotFoundException($"Todo item with ID {todoItem.Id} not found.");

        todoItem.CreatedAt = existingTodo.CreatedAt; 
        todoItem.UpdatedAt = DateTime.UtcNow;

        return await _todoRepository.UpdateAsync(todoItem);
    }

    public async Task<bool> ToggleTodoCompletionAsync(Guid id)
    {
        var exists = await _todoRepository.ExistsAsync(id);
        if (!exists)
            throw new KeyNotFoundException($"Todo item with ID {id} not found.");

        return await _todoRepository.ToggleCompletionAsync(id);
    }

    public async Task<bool> DeleteTodoAsync(Guid id)
    {
        var exists = await _todoRepository.ExistsAsync(id);
        if (!exists)
            throw new KeyNotFoundException($"Todo item with ID {id} not found.");

        return await _todoRepository.DeleteAsync(id);
    }

    public async Task<object> GetTodoStatsAsync()
    {
        var totalTodos = await _todoRepository.GetCountByStatusAsync(true) +
                       await _todoRepository.GetCountByStatusAsync(false);
        var completedTodos = await _todoRepository.GetCountByStatusAsync(true);
        var pendingTodos = await _todoRepository.GetCountByStatusAsync(false);
        var overdueTodos = (await _todoRepository.GetOverdueAsync()).Count();
        var dueTodayTodos = (await _todoRepository.GetDueTodayAsync()).Count();

        return new
        {
            TotalTodos = totalTodos,
            CompletedTodos = completedTodos,
            PendingTodos = pendingTodos,
            OverdueTodos = overdueTodos,
            DueTodayTodos = dueTodayTodos,
            CompletionRate = totalTodos > 0 ? Math.Round((double)completedTodos / totalTodos * 100, 2) : 0
        };
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        return await _todoRepository.GetDistinctCategoriesAsync();
    }
}
