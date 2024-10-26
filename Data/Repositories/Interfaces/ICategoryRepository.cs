using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Data.Repositories.Interfaces;

public interface ICategoryRepository
{
    public Task<ICollection<Category>> GetAllCategoriesAsync();
    public Task<Category?> GetCategoryByIdAsync(long id);
    public Task<ICollection<Category>> GetCategorysByIdsAsync(HashSet<long> ids);
    public Task<Category?> CreateCategoryAsync(Category category);
    public Task<bool> UpdateCategoryAsync(Category category);
    public Task<bool> DeleteCategoryAsync(Category category);
}
