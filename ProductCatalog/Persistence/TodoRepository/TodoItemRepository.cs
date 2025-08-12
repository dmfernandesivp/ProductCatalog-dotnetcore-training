using Domain.TodoApp;
using Persistence.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.TodoRepository;

public class TodoRepository : ITodoRepository
{
    private readonly IDatabaseContext _context;

    public TodoRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id)
    {
        var sql = "SELECT * FROM Todos WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<TodoItem>(sql, new { Id = id });
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        var sql = "SELECT * FROM Todos ORDER BY CreatedAt DESC";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<TodoItem>(sql);
    }

    public async Task<IEnumerable<TodoItem>> GetCompletedAsync(bool isCompleted = true)
    {
        var sql = "SELECT * FROM Todos WHERE IsCompleted = @IsCompleted ORDER BY UpdatedAt DESC";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<TodoItem>(sql, new { IsCompleted = isCompleted });
    }

    public async Task<IEnumerable<TodoItem>> GetByPriorityAsync(string priority)
    {
        var sql = "SELECT * FROM Todos WHERE Priority = @Priority ORDER BY CreatedAt DESC";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<TodoItem>(sql, new { Priority = priority });
    }

    public async Task<IEnumerable<TodoItem>> GetByCategoryAsync(string category)
    {
        var sql = "SELECT * FROM Todos WHERE Category = @Category ORDER BY CreatedAt DESC";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<TodoItem>(sql, new { Category = category });
    }

    public async Task<IEnumerable<TodoItem>> GetDueTodayAsync()
    {
        var sql = "SELECT * FROM Todos WHERE CAST(DueDate AS DATE) = CAST(GETUTCDATE() AS DATE) AND IsCompleted = 0 ORDER BY DueDate";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<TodoItem>(sql);
    }

    public async Task<IEnumerable<TodoItem>> GetOverdueAsync()
    {
        var sql = "SELECT * FROM Todos WHERE DueDate < GETUTCDATE() AND IsCompleted = 0 ORDER BY DueDate";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<TodoItem>(sql);
    }

    public async Task<IEnumerable<TodoItem>> SearchAsync(string searchTerm)
    {
        var sql = "SELECT * FROM Todos WHERE Title LIKE @SearchTerm OR Description LIKE @SearchTerm OR Category LIKE @SearchTerm ORDER BY CreatedAt DESC";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<TodoItem>(sql, new { SearchTerm = $"%{searchTerm}%" });
    }

    public async Task<TodoItem> CreateAsync(TodoItem todoItem)
    {
        var sql = "INSERT INTO Todos (Id, Title, Description, IsCompleted, Priority, Category, DueDate, CreatedAt, UpdatedAt) VALUES (@Id, @Title, @Description, @IsCompleted, @Priority, @Category, @DueDate, @CreatedAt, @UpdatedAt)";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, todoItem);
        return todoItem;
    }

    public async Task<TodoItem> UpdateAsync(TodoItem todoItem)
    {
        var sql = "UPDATE Todos SET Title = @Title, Description = @Description, IsCompleted = @IsCompleted, Priority = @Priority, Category = @Category, DueDate = @DueDate, UpdatedAt = @UpdatedAt WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, todoItem);
        return todoItem;
    }

    public async Task<bool> ToggleCompletionAsync(Guid id)
    {
        var sql = "UPDATE Todos SET IsCompleted = CASE WHEN IsCompleted = 1 THEN 0 ELSE 1 END, UpdatedAt = @UpdatedAt WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UpdatedAt = DateTime.UtcNow });
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = "DELETE FROM Todos WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var sql = "SELECT COUNT(1) FROM Todos WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var count = await connection.QuerySingleAsync<int>(sql, new { Id = id });
        return count > 0;
    }

    public async Task<int> GetCountByStatusAsync(bool isCompleted)
    {
        var sql = "SELECT COUNT(*) FROM Todos WHERE IsCompleted = @IsCompleted";

        using var connection = _context.CreateConnection();
        return await connection.QuerySingleAsync<int>(sql, new { IsCompleted = isCompleted });
    }

    public async Task<IEnumerable<string>> GetDistinctCategoriesAsync()
    {
        var sql = "SELECT DISTINCT Category FROM Todos WHERE Category IS NOT NULL AND Category != '' ORDER BY Category";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<string>(sql);
    }
}
