using Microsoft.EntityFrameworkCore;
using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.DTOs;

namespace prof_edna_teles_shop_api.Data.Repositories;

public class PageRepository : IPageRepository
{
    private readonly AppDbContext _db;

    public PageRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PageResponseDTO> GetHomePageContent()
    {
        PageResponseDTO pageContent = new();

        pageContent.ProductMiniRange = 10;
        pageContent.ProductsMini.AddRange
        (
            await _db.Products
            .OrderByDescending(p => p.Id)
            .Take(pageContent.ProductMiniRange)
            .OrderBy(p => p.Id)
            .Select(p => new ProductMiniResponseDTO(p))
            .ToListAsync()
        );

        pageContent.ProductsMini.AddRange
        (
            await _db.Users
            //Acha o ID dos produtos mais vendidos
            .SelectMany(u => u.Products)
            .GroupBy(p => p.Id)
            .Select(g => new { ProductId = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count)
            .Take(pageContent.ProductMiniRange)
            //Pega os produtos que tem os IDs encontrados
            .Join(_db.Products,
                p => p.ProductId,
                prod => prod.Id,
                (p, prod) => prod)
            .OrderBy(p => p.Id)
            .Select(p => new ProductMiniResponseDTO(p))
            .ToListAsync()
        );

        pageContent.Categories.AddRange
        (
            await _db.Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoryResponseDTO(c))
            .ToListAsync()
        );
        pageContent.CategoryRange = (byte) pageContent.Categories.Count;

        pageContent.ProductsMini.AddRange
        (
            await _db.Products
            .OrderBy(p => Guid.NewGuid())
            .Take(pageContent.ProductMiniRange)
            .OrderBy(p => p.Id)
            .Select(p => new ProductMiniResponseDTO(p))
            .ToListAsync()
        );

        return pageContent;
    }
}
