using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Models;
using System.Globalization;
using System.Text;

namespace prof_edna_teles_shop_api.Data.Repositories;

public class ProductRepository : IProductRepository
{
    IDbContextFactory<AppDbContext> _context;
    public readonly  AppDbContext _db;

    public ProductRepository(IDbContextFactory<AppDbContext> context)
    {
        _db = context.CreateDbContext();
        _context = context;
    }

    public async Task<ICollection<Product>> GetAllProductsAsync()
    {
        return await _db.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    public async Task<ProductFilteredResponseDTO?> GetFilteredPaginatedProducts(string[] terms, int length, int page)
    {
        var products = await _db.Products
            .AsNoTracking()
            .ToListAsync();

        var productsFound = products
            .Where(p => terms.All(t =>
                NormalizeString(p.Name).Contains(NormalizeString(t)) ||
                NormalizeString(p.Description).Contains(NormalizeString(t)) ||
                p.Categories.Any(c => NormalizeString(c.Name).Contains(NormalizeString(t)))
            ))
            .ToList();

        if (productsFound.Count > 0)
        {
            var productList = productsFound
                .Skip(length * (page - 1))
                .Take(length)
                .Select(p => new ProductMiniResponseDTO(p))
                .ToList();

            return new ProductFilteredResponseDTO()
            {
                Length = productsFound.Count,
                Products = productList
            };
        }

        return null;
    }

    private static string NormalizeString(string input)
    {
        string normalized = input.Normalize(NormalizationForm.FormD);
        return new string(normalized
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray())
            .Normalize(NormalizationForm.FormC)
            .ToLower();
    }

    public async Task<int> GetFilteredPaginatedProductsLength(string[] terms)
    {
        return await _db.Products
            .Where(p => terms.Any(t =>
                p.Name.Contains(t) ||
                p.Description.Contains(t)))
            .CountAsync();
    }

    public async Task<Product?> GetProductByIdAsync(long id)
    {
        return await _db.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ICollection<Product>> GetProductsByIdsAsync(HashSet<long> ids)
    {
        return await _db.Products
            .AsNoTracking()
            .Where(p => ids.Contains(p.Id))
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    public async Task<ICollection<Product>> GetRecentProducts(int size)
    {
        return await _db.Products
            .AsNoTracking()
            .OrderByDescending(p => p.Id)
            .Take(size)
            .ToListAsync();
    }

    public async Task<ICollection<Product>> GetRandomProducts(int size)
    {
        return await _db.Products
           .AsNoTracking()
           .OrderBy(p => Guid.NewGuid())
           .Take(size)
           .ToListAsync();
    }

    public async Task<List<string?>> GetAllTypeProducts()
    {
        return await _db.Products
            .AsNoTracking()
            .Select(p => p.GameType)
            .Where(t => t != null)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Product?> CreateProductAsync(Product product)
    {
        EntityEntry<Product> productCreated = _db.Products.Add(product);

        int createdResult = await _db.SaveChangesAsync();

        return (createdResult > 0)
            ? productCreated.Entity
            : null;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        Product? userFound = await GetProductByIdAsync(product.Id);

        if (userFound != null)
        {
            _db.Entry(userFound).CurrentValues.SetValues(product);
            int updatedResult = await _db.SaveChangesAsync();

            return (updatedResult > 0);
        }

        return false;
    }

    public async Task<bool> DeleteProductAsync(Product product)
    {
        _db.Products.Remove(product);
        int deleteResult = await _db.SaveChangesAsync();

        return (deleteResult > 0);
    }
}

