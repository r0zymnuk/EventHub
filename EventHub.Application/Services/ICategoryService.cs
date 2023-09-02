namespace EventHub.Application.Services;
public interface ICategoryService
{
    Task<Category?> GetCategoryByIdAsync(Guid categoryId);
    Task<ICollection<Category>> GetCategoriesAsync();
    Task<ICollection<Category>> GetChildCategoriesAsync(Guid parentId);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Guid categoryId);
}
