using prof_edna_teles_shop_api.DTOs;

namespace prof_edna_teles_shop_api.Services.Interfaces;

public interface IProductService
{
    public Task<ICollection<ProductResponseDTO>> GetAllProductsAsync();
    public Task<ProductResponseDTO?> GetProductByIdAsync(long id);
    public Task<ICollection<ProductResponseDTO>> GetProductsByIdsAsync(HashSet<long> ids);
    public Task<ProductResponseDTO?> CreateProductAsync(ProductPostDTO product);
    public Task<bool> UpdateProductAsync(ProductPutDTO product);
    public Task<bool> DeleteProductByIdAsync(long id);
}
