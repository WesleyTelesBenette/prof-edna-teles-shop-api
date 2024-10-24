using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Services.Interfaces;

public interface IProductService
{
    public Task<ICollection<Product>> GetAllProductAsync();
    public Task<Product?> GetProductByIdAsync(long id);
    public Task<ICollection<Product>> GetProductsByIdsAsync(HashSet<long> ids);
    public Task<Product?> CreateProductAsync(ProductPostDTO product);
    public Task<bool> UpdateProductAsync(ProductPutDTO product);
    public Task<bool> DeleteProductByIdAsync(long id);
}
