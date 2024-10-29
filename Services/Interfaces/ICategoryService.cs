using prof_edna_teles_shop_api.DTOs;

namespace prof_edna_teles_shop_api.Services.Interfaces;

public interface ICategoryService
{
    public Task<ICollection<CategoryResponseDTO>> GetAllCategoriesAsync();
    public Task<CategoryResponseDTO?> GetCategoryByIdAsync(long id);
    public Task<ICollection<CategoryResponseDTO>> GetCategoriesByIdsAsync(HashSet<long> ids);
    public Task<CategoryResponseDTO?> CreateCategoryAsync(CategoryPostDTO category);
    public Task<bool> UpdateCategoryAsync(CategoryPutDTO category);
    public Task<bool> DeleteCategoryByIdAsync(long id);
}
