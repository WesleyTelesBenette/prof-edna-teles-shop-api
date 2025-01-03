﻿using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Data.Repositories.Interfaces;

public interface IProductRepository
{
    public Task<ICollection<Product>> GetAllProductsAsync();
    public Task<ProductFilteredResponseDTO?> GetFilteredPaginatedProducts(string[] terms, int length, int page);
    public Task<int> GetFilteredPaginatedProductsLength(string[] terms);
    public Task<Product?> GetProductByIdAsync(long id);
    public Task<ICollection<Product>> GetProductsByIdsAsync(HashSet<long> ids);
    public Task<ICollection<Product>> GetRecentProducts(int size);
    public Task<ICollection<Product>> GetRandomProducts(int size);
    public Task<List<string?>> GetAllTypeProducts();
    public Task<Product?> CreateProductAsync(Product product);
    public Task<bool> UpdateProductAsync(Product product);
    public Task<bool> DeleteProductAsync(Product product);
}
