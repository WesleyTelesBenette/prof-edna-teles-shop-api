using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<Category>> GetAllCategoriesAsync()
    {
        return await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(long id)
    {
        return await _db.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ICollection<Category>> GetCategoriesByIdsAsync(HashSet<long> ids)
    {
        return await _db.Categories
            .Where(c => ids.Contains(c.Id))
            .OrderBy(c => c.Id)
            .ToListAsync();
    }

    public async Task<bool> IsRepeatedCategoryName(string name)
    {
        return ((await _db.Categories.FirstOrDefaultAsync(c => c.Name == name)) != null);
    }

    public async Task<Category?> CreateCategoryAsync(Category category)
    {
        EntityEntry<Category> categoryCreated = await _db.Categories.AddAsync(category);
        int createdResult = await _db.SaveChangesAsync();

        return (createdResult > 0)
            ? categoryCreated.Entity
            : null;
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        Category? categoryFoud = await GetCategoryByIdAsync(category.Id);

        if (categoryFoud != null)
        {
            _db.Entry(categoryFoud).CurrentValues.SetValues(category);
            int updatedResult = await _db.SaveChangesAsync();

            return (updatedResult > 0);
        }

        return false;
    }

    public async Task<bool> DeleteCategoryAsync(Category category)
    {
        _db.Remove(category);
        int deletedResult = await _db.SaveChangesAsync();

        return (deletedResult > 0);
    }
}
