using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Services.Interfaces;

public interface ICategoryService
{
    public Task<ICollection<Category>> GetAllCategoryAsync();
    public Task<Category?> GetCategoryByIdAsync(long id);
    public Task<ICollection<Category>> GetCategoriesByIdsAsync(HashSet<long> ids);
    public Task<Category?> CreateCategoryAsync(CategoryPostDTO category);
    public Task<bool> UpdateCategoryAsync(CategoryPutDTO category);
    public Task<bool> DeleteCategoryByIdAsync(long id);
}
