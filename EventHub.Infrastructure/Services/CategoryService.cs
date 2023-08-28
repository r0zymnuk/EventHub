using Microsoft.EntityFrameworkCore;

namespace EventHub.Infrastructure.Services;
public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(Guid categoryId)
    {
        Category? category = await GetCategoryByIdAsync(categoryId);
        if (category is not null)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<ICollection<Category>> GetCategoriesAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
    {
        return await _dbContext.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<ICollection<Category>> GetChildCategoriesAsync(Guid parentId)
    {
        return await _dbContext.Categories.
            Where(c => c.Id == parentId)
            .Include(c => c.SubCategories)
            .SelectMany(c => c.SubCategories).ToListAsync();
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        var categoryToUpdate = _dbContext.Categories.Attach(category);
        categoryToUpdate.State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return category;
    }
}
