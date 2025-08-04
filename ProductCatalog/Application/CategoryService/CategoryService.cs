using Domain;
using Persistence.CategoryRepository;

namespace Application.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        category.CreatedAt = DateTime.UtcNow;
        category.IsActive = true;

        return await _categoryRepository.CreateAsync(category);
    }

    public async Task<Category> UpdateCategoryAsync(int id, Category category)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
            throw new ArgumentException("Category not found");

        existingCategory.Name = category.Name;
        existingCategory.Description = category.Description;
        existingCategory.IsActive = category.IsActive;

        return await _categoryRepository.UpdateAsync(existingCategory);
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        return await _categoryRepository.DeleteAsync(id);
    }
}
