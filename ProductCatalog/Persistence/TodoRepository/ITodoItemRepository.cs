using Domain.TodoApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.TodoRepository;

public interface ITodoRepository
{
    Task<TodoItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<IEnumerable<TodoItem>> GetCompletedAsync(bool isCompleted = true);
    Task<IEnumerable<TodoItem>> GetByPriorityAsync(string priority);
    Task<IEnumerable<TodoItem>> GetByCategoryAsync(string category);
    Task<IEnumerable<TodoItem>> GetDueTodayAsync();
    Task<IEnumerable<TodoItem>> GetOverdueAsync();
    Task<IEnumerable<TodoItem>> SearchAsync(string searchTerm);
    Task<TodoItem> CreateAsync(TodoItem todoItem);
    Task<TodoItem> UpdateAsync(TodoItem todoItem);
    Task<bool> ToggleCompletionAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<int> GetCountByStatusAsync(bool isCompleted);
    Task<IEnumerable<string>> GetDistinctCategoriesAsync();
}