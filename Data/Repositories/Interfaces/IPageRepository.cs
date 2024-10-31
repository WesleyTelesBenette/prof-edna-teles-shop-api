using prof_edna_teles_shop_api.DTOs;

namespace prof_edna_teles_shop_api.Data.Repositories.Interfaces;

public interface IPageRepository
{
    public Task<PageResponseDTO> GetHomePageContent();
}
