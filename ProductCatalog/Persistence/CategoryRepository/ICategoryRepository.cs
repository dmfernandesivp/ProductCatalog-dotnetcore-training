using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.CategoryRepository;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category> CreateAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task<bool> DeleteAsync(int id);
}
