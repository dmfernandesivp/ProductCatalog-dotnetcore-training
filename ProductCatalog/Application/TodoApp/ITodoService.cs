using Domain.TodoApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoApp;

public interface ITodoService
{
    Task<TodoItem?> GetTodoByIdAsync(Guid id);
    Task<IEnumerable<TodoItem>> GetAllTodosAsync();
    Task<IEnumerable<TodoItem>> GetCompletedTodosAsync();
    Task<IEnumerable<TodoItem>> GetPendingTodosAsync();
    Task<IEnumerable<TodoItem>> GetTodosByPriorityAsync(string priority);
    Task<IEnumerable<TodoItem>> GetTodosByCategoryAsync(string category);
    Task<IEnumerable<TodoItem>> GetTodosDueTodayAsync();
    Task<IEnumerable<TodoItem>> GetOverdueTodosAsync();
    Task<IEnumerable<TodoItem>> SearchTodosAsync(string searchTerm);
    Task<TodoItem> CreateTodoAsync(TodoItem todoItem);
    Task<TodoItem> UpdateTodoAsync(TodoItem todoItem);
    Task<bool> ToggleTodoCompletionAsync(Guid id);
    Task<bool> DeleteTodoAsync(Guid id);
    Task<object> GetTodoStatsAsync();
    Task<IEnumerable<string>> GetCategoriesAsync();
}
