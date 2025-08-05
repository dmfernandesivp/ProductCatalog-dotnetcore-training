using Dapper;
using Domain;
using Persistence.Utilities;

namespace Persistence.CategoryRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly IDatabaseContext _context;

    public CategoryRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Categories WHERE IsActive = 1";

        var connection = _context.CreateConnection();
        return await connection.QueryAsync<Category>(sql);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM Categories WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
    }

    public async Task<Category> CreateAsync(Category category)
    {
        const string sql = @"
                INSERT INTO Categories (Name, Description, CreatedAt, IsActive)
                VALUES (@Name, @Description, @CreatedAt, @IsActive);
                SELECT CAST(SCOPE_IDENTITY() as int)";

        using var connection = _context.CreateConnection();
        var id = await connection.QuerySingleAsync<int>(sql, category);
        category.Id = id;
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        const string sql = @"
                UPDATE Categories 
                SET Name = @Name, Description = @Description, IsActive = @IsActive
                WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, category);
        return category;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Categories WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }
}