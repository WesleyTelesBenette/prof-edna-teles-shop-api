using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.DTOs;
using System.Linq;

namespace prof_edna_teles_shop_api.Data.Repositories;

public class PageRepository : IPageRepository
{
    IDbContextFactory<AppDbContext> _db;



    public PageRepository(IDbContextFactory<AppDbContext> db)
    {
        _db = db;
    }

    public async Task<PageResponseDTO> GetPageHomContent(PageResponseDTO page)
    {
        using var context1 = _db.CreateDbContext();
        using var context2 = _db.CreateDbContext();
        using var context3 = _db.CreateDbContext();
        using var context4 = _db.CreateDbContext();
        using var context5 = _db.CreateDbContext();

        var recentProductsTask = context1.Products
            .AsNoTracking()
            .OrderByDescending(p => p.Id)
            .Take(10)
            .Select(p => new ProductMiniResponseDTO(p))
            .ToListAsync();

        var expensiveProductsTask = context2.Products
            .AsNoTracking()
            .OrderByDescending(p => p.PriceInCents)
            .Take(10)
            .Select(p => new ProductMiniResponseDTO(p))
            .ToListAsync();

        var cheapProductsTask = context3.Products
            .AsNoTracking()
            .OrderBy(p => p.PriceInCents)
            .Take(10)
            .Select(p => new ProductMiniResponseDTO(p))
            .ToListAsync();

        var categoriesTask = context4.Categories
            .AsNoTracking()
            .Select(c => new CategoryResponseDTO(c))
            .ToListAsync();

        var gameTypesTask = context5.Products
            .AsNoTracking()
            .Where(p => p.GameType != null)
            .Select(p => p.GameType)
            .Distinct()
            .ToListAsync();

        // Executa todas as consultas simultaneamente
        await Task.WhenAll(recentProductsTask, expensiveProductsTask, cheapProductsTask, categoriesTask, gameTypesTask);

        // Popula o DTO com os resultados
        page.ProductsMini.Add(await recentProductsTask);
        page.ProductsMini.Add(await expensiveProductsTask);
        page.ProductsMini.Add(await cheapProductsTask);
        page.Categories.Add(await categoriesTask);
        page.Strings.Add([.. await gameTypesTask]);

        return page;
    }
}
