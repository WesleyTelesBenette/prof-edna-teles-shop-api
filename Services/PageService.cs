using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Services.Interfaces;

namespace prof_edna_teles_shop_api.Services;

public class PageService : IPageService
{
    private readonly IProductRepository _productRep;
    private readonly ICategoryRepository _categoryRep;

    public PageService(IProductRepository productRep, ICategoryRepository categoryRep)
    {
        _productRep = productRep;
        _categoryRep = categoryRep;
    }

    public async Task<PageResponseDTO?> GetHomePageContent()
    {
        PageResponseDTO pageContent = new()
        {
            Name = "Home Page",
            Version = 1
        };

        pageContent.ProductsMini[0] = (await _productRep.GetRecentProducts(10)).Select(p => new ProductMiniResponseDTO(p)).ToArray();
        pageContent.ProductsMini[1] = (await _productRep.GetRandomProducts(10)).Select(p => new ProductMiniResponseDTO(p)).ToArray();
        pageContent.ProductsMini[2] = (await _productRep.GetRandomProducts(10)).Select(p => new ProductMiniResponseDTO(p)).ToArray();

        pageContent.Categories[0] = (await _categoryRep.GetAllCategoriesAsync()).Select(p => new CategoryResponseDTO(p)).ToArray();
        pageContent.Strings[0] = [.. (await _productRep.GetAllTypeProducts())];

        return pageContent;
    }
}
