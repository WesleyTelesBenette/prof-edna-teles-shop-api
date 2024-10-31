using prof_edna_teles_shop_api.DTOs;

namespace prof_edna_teles_shop_api.Services.Interfaces;

public interface IPageService
{
    public Task<PageResponseDTO?> GetHomePageContent();
}
