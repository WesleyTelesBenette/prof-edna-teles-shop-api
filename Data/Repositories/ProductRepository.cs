using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<Product>> GetAllProductsAsync()
    {
        return await _db.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync();
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

