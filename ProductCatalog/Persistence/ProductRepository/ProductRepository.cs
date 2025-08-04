using Dapper;
using Domain;
using Persistence.Utilities;

namespace Persistence.ProductRepository;

public class ProductRepository : IProductRepository
{
    private readonly IDatabaseContext _context;

    public ProductRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        const string sql = @"
                SELECT p.*, c.Name as Category 
                FROM Products p 
                INNER JOIN Categories c ON p.CategoryId = c.Id 
                WHERE p.IsActive = 1";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Product>(sql);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        const string sql = @"
                SELECT p.*, c.Name as Category 
                FROM Products p 
                INNER JOIN Categories c ON p.CategoryId = c.Id 
                WHERE p.Id = @Id";

        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
    }

    public async Task<Product> CreateAsync(Product product)
    {
        const string sql = @"
                INSERT INTO Products (Name, Description, Price, CategoryId, CreatedAt, IsActive)
                VALUES (@Name, @Description, @Price, @CategoryId, @CreatedAt, @IsActive);
                SELECT CAST(SCOPE_IDENTITY() as int)";

        using var connection = _context.CreateConnection();
        var id = await connection.QuerySingleAsync<int>(sql, product);
        product.Id = id;
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        const string sql = @"
                UPDATE Products 
                SET Name = @Name, Description = @Description, Price = @Price, 
                    CategoryId = @CategoryId, UpdatedAt = @UpdatedAt, IsActive = @IsActive
                WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, product);
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Products WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        const string sql = @"
                SELECT p.*, c.Name as Category 
                FROM Products p 
                INNER JOIN Categories c ON p.CategoryId = c.Id 
                WHERE p.CategoryId = @CategoryId AND p.IsActive = 1";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Product>(sql, new { CategoryId = categoryId });
    }
}
