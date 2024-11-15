using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Services.Interfaces;

namespace prof_edna_teles_shop_api.Services;

public class PageService : IPageService
{
    private readonly IPageRepository _pageRep;

    public PageService(IPageRepository pageRep)
    {
        _pageRep = pageRep;
    }

    public async Task<PageResponseDTO?> GetHomePageContent()
    {
        PageResponseDTO pageContent = new()
        {
            Name = "Home Page",
            Version = 1
        };

        return await _pageRep.GetPageHomContent(pageContent);
    }
}
